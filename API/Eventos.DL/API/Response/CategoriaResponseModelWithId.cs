namespace Eventos.DL.API.Response {
    public class CategoriaResponseModelWithId {
        public CategoriaResponseModelWithId(string nome, int id) {
            Id = id;
            NomeCategoria = nome;
        }

        public int Id { get; }
        public string NomeCategoria { get; }
    }
}