using HodorVault.Data.Models;
using HodorVault.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

using Dapper;
using static Dapper.SqlBuilder;
using System.Linq;
using System.Data.Common;
using System;

namespace HodorVault.Data.Services
{
    public interface IVaultService
    {
        Task<EntryResult> InsertOrUpdateEntry(Artist artist, Album album);
        Task<IList<Artist>> GetAllEntries();
    }

    public class VaultService : IVaultService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IRepository<Artist> _artistRepository;
        private readonly IRepository<Album> _albumRepository;

        public VaultService(IConnectionFactory connectionFactory, IRepository<Artist> artistRepository, IRepository<Album> albumRepository)
        {
            _connectionFactory = connectionFactory;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
        }

        public async Task<EntryResult> InsertOrUpdateEntry(Artist artist, Album album)
        {
            var builder = new SqlBuilder();

            Template mergeArtistTemplate = builder.AddTemplate(@"
                merge Artists as a
                using
                    (values (@id, @name, @country)) as source (id, name, country)
                on a.Id = source.id
                when not matched then
                    insert (id, name, country)
                    values (source.id, source.name, source.country)
                when matched then update set
	                a.name = source.name,
	                a.country = source.country
                output
                    $action as Action,
                    inserted.Id;",
                new { id = artist.Id, name = artist.Name, country = artist.Country });

            Template mergeAlbumTemplate = builder.AddTemplate(@"
                merge Albums as a
                using
                    (values (@id, @name, @type, @released, @artistId)) as source (id, name, type, released, artistId)
                on a.Id = source.id
                when not matched then
                    insert (id, name, type, released, artistId)
                    values (source.id, source.name, source.type, source.released, source.artistId)
                when matched then update set
	                a.name = source.name,
	                a.type = source.type,
                    a.released = source.released,
                    a.artistId = source.artistId
                output
                    $action as Action,
                    inserted.Id;",
                new { id = album.Id, name = album.Name, type = album.Type, released = album.Year, artistId = artist.Id });

            dynamic artistResult = null;
            dynamic albumResult = null;

            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                try
                {
                    artistResult = await connection.QuerySingleAsync<dynamic>(mergeArtistTemplate.RawSql, mergeArtistTemplate.Parameters);
                }
                catch (Exception e)
                {
                    artistResult = new { Action = "ERROR: " + e.Message, Id = 0 };
                }
                try
                {
                    albumResult = await connection.QuerySingleAsync<dynamic>(mergeAlbumTemplate.RawSql, mergeAlbumTemplate.Parameters);
                }
                catch  (Exception e)
                {
                    albumResult = new { Action = "ERROR: " + e.Message, Id = 0 };
                }
            }

            return new EntryResult
            {
                Artists = new MergeResult(artistResult.Action, artistResult.Id),
                Albums = new MergeResult(albumResult.Action, albumResult.Id)
            };
        }

        public async Task<IList<Artist>> GetAllEntries()
        {
            IList<Artist> result = new List<Artist>();
            var sql = @"select
                            a.id as Id,
                            a.name as Name,
                            a.country as Country,
                            b.id as Albums_Id,
                            b.name as Albums_Name,
                            b.type as Albums_Type,
                            b.released as Albums_Year,
                            b.comment as Albums_Comment,
                            b.artistid as Albums_ArtistId
                        from artists a
                        left join albums b on a.Id = b.artistId";

            using (DbConnection connection = await _connectionFactory.GetOpenConnectionAsync())
            {
                var entries = await connection.QueryAsync<dynamic>(sql);

                // slapper.automapper maps by convention to Artist.Albums
                result = (Slapper.AutoMapper.MapDynamic<Artist>(entries) as IEnumerable<Artist>).ToList();
            }

            return result;
        }
    }
}
