using CRUDAPI.Models;
using CRUDAPI.Services;
using CRUDAPI.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CRUDAPITest
{
    [TestClass]
    public class ProdutoTest
    {
        private Mock<ProdutoDbContext> _mockContext;
        private ProdutoService _produtoService;
        private List<Produto> _produtos;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProdutoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<ProdutoDbContext>(options);
            _produtos = new List<Produto>
            {
                new Produto { ProductID = 1, Name = "Produto1", Price = 10, StockQuantity = 100 },
                new Produto { ProductID = 2, Name = "Produto2", Price = 20, StockQuantity = 200 }
            };

            var mockSet = new Mock<DbSet<Produto>>();
            mockSet.As<IQueryable<Produto>>().Setup(m => m.Provider).Returns(_produtos.AsQueryable().Provider);
            mockSet.As<IQueryable<Produto>>().Setup(m => m.Expression).Returns(_produtos.AsQueryable().Expression);
            mockSet.As<IQueryable<Produto>>().Setup(m => m.ElementType).Returns(_produtos.AsQueryable().ElementType);
            mockSet.As<IQueryable<Produto>>().Setup(m => m.GetEnumerator()).Returns(_produtos.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Produtos).Returns(mockSet.Object);
            _produtoService = new ProdutoService(_mockContext.Object);
        }

        [TestMethod]
        public async Task CreateProdutoAsync_ShouldAddProduto()
        {
            var newProduto = new Produto { ProductID = 3, Name = "Produto3", Price = 30, StockQuantity = 300 };

            var result = await _produtoService.CreateProdutoAsync(newProduto);

            Assert.AreEqual(newProduto, result);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Preço deve ser maior que 0.")]
        public async Task CreateProdutoAsync_ShouldThrowException_WhenPriceIsZeroOrLess()
        {
            var newProduto = new Produto { ProductID = 3, Name = "Produto3", Price = 0, StockQuantity = 300 };

            await _produtoService.CreateProdutoAsync(newProduto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Quantidade em estoque deve ser igual ou maior que 0.")]
        public async Task CreateProdutoAsync_ShouldThrowException_WhenStockQuantityIsLessThanZero()
        {
            var newProduto = new Produto { ProductID = 3, Name = "Produto3", Price = 30, StockQuantity = -1 };

            await _produtoService.CreateProdutoAsync(newProduto);
        }

        [TestMethod]
        public async Task GetAllProdutosAsync_ShouldReturnAllProdutos()
        {
            var result = await _produtoService.GetAllProdutosAsync();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Produto1", result[0].Name);
            Assert.AreEqual("Produto2", result[1].Name);
        }

        [TestMethod]
        public async Task GetProdutoByIdAsync_ShouldReturnProduto_WhenIdExists()
        {
            var result = await _produtoService.GetProdutoByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Produto1", result.Name);
        }

        [TestMethod]
        public async Task GetProdutoByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var result = await _produtoService.GetProdutoByIdAsync(3);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateProdutoAsync_ShouldUpdateProduto()
        {
            var updatedProduto = new Produto { ProductID = 1, Name = "UpdatedProduto1", Price = 15, StockQuantity = 150 };

            var result = await _produtoService.UpdateProdutoAsync(1, updatedProduto);

            Assert.AreEqual(updatedProduto.Name, result.Name);
            Assert.AreEqual(updatedProduto.Price, result.Price);
            Assert.AreEqual(updatedProduto.StockQuantity, result.StockQuantity);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteLogByIdAsync_ShouldDeleteProduto()
        {
            var result = await _produtoService.DeleteLogByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Produto1", result.Name);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}

