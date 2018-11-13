using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//add reference to web projects
using MusicStore.Controllers;
using Moq;
using MusicStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Tests.Controllers
{
    
[TestClass]
    public class AlbumsControllerTest
    {
        //Global variables declared to access common mock data
        Mock<IAlbumsMock> mock;
        List<Album> albums;
        AlbumsController albumsController;


        [TestInitialize]
        public void TestInitialize()
        {
            //arrange mock data for all unit tests
            mock = new Mock<IAlbumsMock>();

            albums = new List<Album>
            {
                new Album {AlbumId = 100, Title = "One Hundred", Price = 6.99m, Artist = new Artist{
                ArtistId = 4000, Name="Some One"} },
                new Album {AlbumId = 200, Title = "Two Hundred", Price = 7.99m, Artist = new Artist{
                ArtistId = 4000, Name="Eminem"}},
                new Album {AlbumId = 300, Title = "Three Hundred", Price = 8.99m, Artist = new Artist{
                ArtistId = 4000, Name="Drake"}}
            };

            //populate interface from the mock data
            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());

            albumsController = new AlbumsController(mock.Object);
        }

        [TestMethod]
        public void IndexReturnsView()
        {
            //act
            ViewResult result = albumsController.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsAlbums() {
            // act - does the view results model equal a list of albums
            var actual = (List<Album>)((ViewResult)albumsController.Index()).Model;

            //assert
            CollectionAssert.AreEqual(albums.OrderBy(a => a.Artist.Name).ThenBy(a => a.Title).ToList(), actual);
        }
    }
}
