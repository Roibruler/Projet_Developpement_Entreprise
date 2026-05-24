namespace UrgenceTech.Tests
{
	public class AuthServiceTests
	{
		[Fact]
		public void isConnexion_Desactiver_erreu()
		{
			var repo = new FakeAuthRepository();
			AuthService.SetRepository(repo);

			var user = repo.CreerCompte("Emile Tardif", "emile@yahoo.com", "Banana13!");
			user.Status = false;

			var (succes, message) = AuthService.SeConnecterAsync("emile@yahoo.com", "Banana13!").Result;

			Assert.False(succes);
			Assert.Equal("Mauvais identifiants", message);
		}

		[Fact]
		public void isConnexion_Actif_Success()
		{
			var repo = new FakeAuthRepository();
			AuthService.SetRepository(repo);

			var user = repo.CreerCompte("Emile Tardif", "emile@yahoo.com", "Banana13!");
			user.Status = true;

			var (succes, message) = AuthService.SeConnecterAsync("emile@yahoo.ca", "Banana13!").Result;

			Assert.True(succes);
			Assert.Null(message);
		}
	}
}