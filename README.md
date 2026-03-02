# 🛠️ Gerenciador de Produtos

O **Gerenciador de Produtos** é uma aplicação web que permite criar, listar, editar e remover produtos de forma simples e eficiente.

O projeto possui três telas principais:

1. **Home** – Tela inicial.
2. **Cadastro de Produtos** – Tela para criação de novos produtos.
3. **Listagem de Produtos** – Tela para visualizar produtos cadastrados, com modal para edição.

O front-end é desenvolvido em **Angular** e o back-end em **.NET**, se comunicando via HTTP.

## 🚀 Como Executar

### 1. Back-End (.NET)

1. Abra o projeto no Visual Studio ou VS Code.
2. Execute o backend:

```bash
dotnet run
```

3. A API estará disponível em:

```
http://localhost:5048/
```
### 2. Front-End (Angular)

1. Instale as dependências:

```bash
npm install
```

2. Execute o servidor Angular:

```bash
ng serve
```

3. Acesse no navegador:

```
http://localhost:4200
```

---

## 💡 Observações e Pontos de melhorias futuras

 1. Observações

* Certifique-se de que o backend esteja em execução antes de iniciar o front-end.
* O front-end consome o backend via HTTP.
* Todo o tráfego de dados utiliza JSON.

 2. Proximos passos com mais tempo de trabalho

- Tratamento de erros consistente: Retornar erros padronizados via middleware ou filtro de exceção.

- Tratamento nas mensagens de erro e de sucesso no front-end para melhorar a experiencia do usuario

- Atualmente estou utilizando o InMemory do Entity Framework; consideraria usar SQL Server ou PostgreSQL.

- Adicionar login e roles (ex.: admin, usuário) para controlar quem pode criar ou excluir produtos.

- Evitaria o possivel retorno de centenas de produtos de uma vez; aplicando paginacao.


---
