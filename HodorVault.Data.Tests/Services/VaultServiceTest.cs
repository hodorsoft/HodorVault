using HodorVault.Data.Services;
using NUnit.Framework;
using Moq;
using HodorVault.Data.Repository;
using HodorVault.Data.Models;
using System.Threading.Tasks;

namespace HodorVault.Data.Tests.Services
{
    [TestFixture]
    class VaultServiceTest
    {
        private IRepository<Artist> _artistRepository;
        private IRepository<Album> _albumRepository;
        private IVaultService _target;

        [OneTimeSetUp]
        public void Setup()
        {
            var cf = new ConnectionFactory(TestDbHelper.ConnectionString);
            TestDbHelper.Initialize();

            _artistRepository = new Mock<IRepository<Artist>>().Object;
            _albumRepository = new Mock<IRepository<Album>>().Object;
            _target = new VaultService(cf, _artistRepository, _albumRepository);
        }

        [TearDown]
        public void TearDown()
        {
            TestDbHelper.TearDown();
        }

        [Test]
        public async Task AddEntry_ShouldAddNewEntry()
        {
            var artist = new Artist
            {
                Id = 666,
                Country = "Winterfell",
                Name = "Hodor"
            };
            var album = new Album
            {
                Id = 555,
                ArtistId = artist.Id,
                Name = "Hodor songs",
                Type = "EP"
            };

            var result = await _target.InsertOrUpdateEntry(artist, album);

            Assert.That(result.Artists.Inserted[0], Is.EqualTo(666));
            Assert.That(result.Albums.Inserted[0], Is.EqualTo(555));
            Assert.That(result.Artists.Error, Is.Null);
        }

        [Test]
        public async Task AddEntry_ShouldUpdateExistingEntry()
        {
            var artist = new Artist { Id = 666, Country = "Winterfell", Name = "Hodor" };
            var album = new Album { Id = 555, ArtistId = artist.Id, Name = "Hodor songs", Type = "EP" };
            var albumUpdate = new Album { Id = 555, ArtistId = artist.Id, Name = "Hodor songs (re-release)", Type = "EP" };

            var insertResult = await _target.InsertOrUpdateEntry(artist, album);
            var updateResult = await _target.InsertOrUpdateEntry(artist, albumUpdate);

            Assert.That(updateResult.Albums.Updated[0], Is.EqualTo(555));
        }
    }
}
