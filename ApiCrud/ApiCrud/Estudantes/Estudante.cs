namespace ApiCrud.Estudantes
{
    public class Estudante
    {

        //Guid gera uma sequencia de numeros aleatorios
        //init - dps de setado nao pode ser alterado nunca
        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        
        public Estudante(string nome) 
        {
            //vai gerar um novo guid
            Id = Guid.NewGuid();
            Nome = nome;
            Ativo = true;
        }

    }
}
