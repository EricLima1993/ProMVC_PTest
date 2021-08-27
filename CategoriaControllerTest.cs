using Microsoft.EntityFrameworkCore;
using Moq;
using ProMVC.Models;
using ProMVC_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProMVC_PTest
{
    public class CategoriaControllerTest
    {
        private readonly Mock<DbSet<CategoriaModel>> _mockSet;
        private readonly Mock<Context> _mockContext;
        private readonly CategoriaModel _categoria;
        public CategoriaControllerTest()
        {
            _mockSet = new Mock<DbSet<CategoriaModel>>();
            _mockContext = new Mock<Context>();
            _categoria = new CategoriaModel { Id = 1, Descricao = "Teste" };

            _mockContext.Setup(m => m.Categoria).Returns(_mockSet.Object);

            _mockContext.Setup(m => m.Categoria.FindAsync(1)).ReturnsAsync(_categoria);

            _mockContext.Setup(m => m.SetModified(_categoria));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriaController(_mockContext.Object);
            
            await service.GetCategoriaModel(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once());
        }
        [Fact]
        public async Task Put_Categoria()
        {
            var service = new CategoriaController(_mockContext.Object);

            await service.PutCategoriaModel(1, _categoria);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),Times.Once);
        }
        [Fact]
        public async Task Post_Categoria()
        {
            var service = new CategoriaController(_mockContext.Object);

            await service.PostCategoriaModel(_categoria);

            _mockSet.Verify(x => x.Add(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task Delete_Categoria()
        {
            var service = new CategoriaController(_mockContext.Object);

            await service.DeleteCategoriaModel(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once);
            _mockSet.Verify(m => m.Remove(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
