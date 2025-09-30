<h3><b>DesafioTecnico</b></h3>

<!---->
<h4>Conteúdo</h4>
<ul>
	<li><a href="#descricao">Descrição</a><br></li>
	<li><a href="#instrucoes">Instruções</a><br></li>
	<li><a href="#rodar">Como rodar a aplicação </a><br></li>
	<li><a href="#testar">Como testar a aplicação </a><br></li>
</ul>



<!---->
<h3 id="descricao">Descrição</h3>
<p> Esse projeto foi desenvolvido para avaliação de código através do desafio Instituição Financeira.
Foi decidido utilizar uma aplicação de uma api devido aos requisitos técnicos.
  Foram utilizados pilares de POO, Clean Code e SOLID.
  A solução está conteinerizada e na pasta raiz.
  Foram adicionados testes unitários das camadas de Repositório, Service e UseCase.
  Foram atendidos os requisitos a seguir:
  <li>API em C# /ASP.NET Core 8+.</li>
  <li>Banco de dados: relacional (PostgreSQL).</li>
  <li>Documentação de endpoints com Swagger.</li>
  <li>Testes automatizados para regras de negócio.</li>
  <li>Projeto containerizado com Docker + docker-compose.</li>
  <li>Logs estruturados (mínimo: request/response + erros) com Correlation ID.</li>
  <li>Autenticação via JWT.</li>
  <li>Uso de DTOs e separação clara de camadas (ex.: Controllers, Services, Repositórios).</li>
</p>


<!---->
<h3 id="instrucoes">Instruções</h3>
<ul style="list-style: none;">  
	<li></li>
		<ul style="list-style: none;"> 
           A rota de Criação de contrato está utilizando método de autorização JWT.

           
			Dentro do projeto Intituicao.Financeira utilizar o console para executar o comando Update-Database Init. Será espelhado a modelagem no dados no banco.
			Os scripts de inserção de dados estão localizados em: Intituicao.Financeira.Application.Shared.Repository.Scripts. Caso seja necessário, executá-los na ordem.
		</ul>    
    <li></li>
		<ul style="list-style: none;">
			<li>InsertCondicaoVeiculo.sql</li>	
			<li>InsertTipoVeiculo.sql</li>	
			<li>InsertStatusPagamento.sql</li>	
			<li>InsertContrato.sql</li>	
			<li>InsertPagamentos.sql</li>	
		</ul>	
        <li></li>   		
</ul>

<!---->
<h3 id="rodar">Como rodar a aplicação</h3>
<p>Utilizando a Prompt de comando:</p>
<ul style="list-style: none;">	
	<li>Abrir o projeto</li>
		<ul style="list-style: none;">	
			<li> O projeto pode ser aberto atraves do Visual Studio Code </li>
		</ul>	
	<li>Acessar a pasta do Projeto</li>
		<ul style="list-style: none;">	
			<li> no Terminal do cd [pasta onde a solução foi clonada]\\DesafioTecnico </li>
		</ul>	
	<li>Buildar o projeto</li>
		<ul style="list-style: none;">	
			<li>	Execute os comandos docker build -t desafiotecnico -f DockerFile . </li>
		</ul>	
	<li>Verifique a imagem e crie o container </li>
		<ul style="list-style: none;">	
			<li>	Execute o comando docker images. Em seguida o comando docker create desafiotecnico. </li>
		</ul>	
	<li>Executar a aplicação</li>
		<ul style="list-style: none;">
			<li>	Execute o comando docker ps -a e finalmente copie o nome gerado em NAMES e execute o comando docker start -i {nome}</li>
		</ul>
</ul>	

<h3 id="Testar">Como testar a aplicação</h3>
<p>Utilizando a Prompt de comando ou Terminal do VsCode:</p>
<ul style="list-style: none;">	
	<li>Referências</li>
		<ul style="list-style: none;">	
			<li> Os testes foram feitos para os repositórios, service e usecases.</li>
		</ul>		
	<li>Acessar a pasta do Projeto</li>
		<ul style="list-style: none;">	
			<li> no Terminal do cd [diretorio da solucao]\Instituicao.Financeira.Tests </li>
		</ul>	
	<li>Buildar o projeto</li>
		<ul style="list-style: none;">	
			<li>	Execute os comandos dotnet restore --project Instituicao.Financeira.Tests.csproj. Em seguida dotnet build --project Instituicao.Financeira.Tests.csproj </li>
		</ul>	
	<li>Testas o projeto</li>
		<ul style="list-style: none;">	
			<li>	dotnet test --project Instituicao.Financeira.Tests.csproj </li>
		</ul>	
</ul>	

<!---->