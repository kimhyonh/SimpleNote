using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using SimpleNote.Controllers;
using SimpleNote.Entities;
using SimpleNote.Entities.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleNote.Tests
{
    public class NoteControllerTests
    {

        private Mock<IPersistNote> _noteRepo;
        private NoteController _controller;

        [SetUp]
        public void Setup()
        {
            _noteRepo = new Mock<IPersistNote>();
            _controller = new NoteController(_noteRepo.Object);
        }

        [Test, AutoData]
        public async Task GetAllNotes_ShouldSucceed_WhenRouteIsCorrect(IEnumerable<Note> notes)
        {
            _noteRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(notes));

            var response = await _controller.Get();

            OkObjectResult result = response.Result.ShouldBeOfType<OkObjectResult>();
            result.Value.ShouldBe(notes);
        }

        [Test]
        public async Task CreateNote_ShouldFailed_WhenRequestIsNotValid()
        {
            _controller.ModelState.AddModelError("Value", "No value in a the request body");

            var result = await _controller.Post(null);

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test, AutoData]
        public async Task CreateNote_ShouldSucceed_WhenRequestIsValid(string text)
        {
            _noteRepo.Setup(r => r.Insert(It.IsAny<string>()))
                .Returns(Task.FromResult(new Note()));

            var result = await _controller.Post(text);

            result.ShouldBeOfType<CreatedAtActionResult>();
        }

        [Test, AutoData]
        public async Task UpdateNote_ShouldFailed_WhenRequestIsNotValid()
        {
            _controller.ModelState.AddModelError("Id", $"Id doesn't exist");

            var result = await _controller.Put(It.IsAny<int>(), It.IsAny<string>());

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test, AutoData]
        public async Task UpdateNote_ShouldFailed_WhenIdDoesNotExist(Note note)
        {
            _noteRepo.Setup(r => r.GetById(note.Id))
                .Returns(Task.FromResult<Note>(null));

            var result = await _controller.Put(note.Id, note.Text);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Test, AutoData]
        public async Task UpdateNote_ShouldSucceed_WhenRequestIsValid(Note note)
        {
            _noteRepo.Setup(r => r.GetById(note.Id))
                .Returns(Task.FromResult(note));
            _noteRepo.Setup(r => r.Update(note))
                .Returns(Task.CompletedTask);

            var result = await _controller.Put(note.Id, note.Text);

            result.ShouldBeOfType<NoContentResult>();
        }

        [Test, AutoData]
        public async Task DeleteNote_ShouldFailed_WhenRequestIsNotValid()
        {
            _controller.ModelState.AddModelError("Id", $"Id doesn't exist");

            var result = await _controller.Delete(It.IsAny<int>());

            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Test, AutoData]
        public async Task DeleteNote_ShouldFailed_WhenIdDoesNotExist(int id)
        {
            _noteRepo.Setup(r => r.GetById(id))
                .Returns(Task.FromResult<Note>(null));

            var result = await _controller.Delete(id);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Test, AutoData]
        public async Task DeleteNote_ShouldSucceed_WhenRequestIsValid(int id, Note note)
        {
            _noteRepo.Setup(r => r.GetById(id))
                .Returns(Task.FromResult(note));
            _noteRepo.Setup(r => r.DeleteById(id))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            result.ShouldBeOfType<NoContentResult>();
        }
    }
}