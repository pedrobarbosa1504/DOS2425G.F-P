using Microsoft.EntityFrameworkCore;

public class UserControllerTests
{
    private readonly UserController _controller;

    public UserControllerTests()
    {
        // Configura o banco de dados em memória
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb")  // Define um nome para o banco em memória
                        .Options;

        // Cria uma instância do contexto usando as opções configuradas
        var context = new ApplicationDbContext(options);

        // Cria uma instância do controlador com o contexto em memória
        _controller = new UserController(context);
    }

    // Exemplo de teste
    [Fact]
    public void GetUsers_ReturnsOkResult()
    {
        // Arrange - Preparar o que for necessário, como inserir dados no banco de dados em memória

        // Act - Chama o método que você quer testar
        var result = _controller.GetUsers();

        // Assert - Verifica se o resultado é o esperado
        Assert.IsType<OkObjectResult>(result);
    }
}
