<h1 align="center">Girl Tamagotchi</h1>
<p href="#descricao" align="center">Projeto final da disciplina de Linguagens de Programação, um jogo baseado nos famosos tamagotchis antigos.</p>

<div align="center">
  <img src="https://img.shields.io/badge/mysql-%2300f.svg?&style=for-the-badge&logo=mysql&logoColor=white">
  <img src="https://img.shields.io/badge/typescript-%23007ACC.svg?&style=for-the-badge&logo=typescript&logoColor=white">
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?&style=for-the-badge&logo=c-sharp&logoColor=white">
</div>

<p align="center">
 <a href="#descricao">Objetivo</a> •
 <a href="#roadmap">Roadmap</a> • 
 <a href="#tecnologias">Tecnologias</a> • 
 <a href="#contribuicao">Contribuição</a> • 
 <a href="#licenc-a">Licença</a> • 
 <a href="#autor">Autor</a>
</p>

<h4 align="center"> 
  ✅  Projeto finalizado ✅
</h4>

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
- [Git](https://git-scm.com);
- [Node.js](https://nodejs.org/en/);
- [MySQL v5.7.24](https://downloads.mysql.com/archives/installer/) (Windows) ou [Docker Compose](https://docs.docker.com/compose/install/) (Linux);
- [Mono version (C# support)](https://godotengine.org/download)

Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/)

### 🎲 Baixando os arquivos
```bash
# Clone este repositório
$ git clone <https://github.com/EnzoItaliano/Tamagotchi-LP>

# Acesse a pasta do projeto no terminal/cmd
$ cd Tamagotchi-LP
```
### 🎲 Rodando o Banco de Dados
Pelo Linux
```bash
# Vá para a pasta server
$ cd server

# Execute o docker-compose.yml
$ sudo docker-compose up -d
```
Pelo Windows utilize algum programa para entrar no banco de dados como MySQL Workbench e execute:
```mysql
CREATE DATABASE tamagotchi;

USE tamgotchi
```
### 🎲 Rodando o Back End (servidor)

```bash
# Vá para a pasta server
$ cd server

# Instale as dependências
$ npm install

# Execute a aplicação
$ npm start

# O servidor inciará na porta:3000 - acesse <http://localhost:3000>
```
### 🎲 Rodando a Front End (aplicação)

1. Abra o Godot e clique na opção "scan" no menu lateral.
2. Localize a pasta App e selecione o arquivo project.godot, com isso ele abrirá o projeto.
3. Clique no ícone de play no canto superior direito e ele abrirá o jogo.

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [MySQL](https://dev.mysql.com)
- [Docker](https://www.docker.com)
- [Node.js](https://nodejs.org/en/)
- [TypeScript](https://www.typescriptlang.org/)
- [Express](https://expressjs.com/pt-br/)
- [TypeORM](https://typeorm.io/#/)
- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [Godot](https://godotengine.org)

### Autores
---
<table>
  <tr>
    <td align="center"><a href="https://www.linkedin.com/in/enzoitaliano/"><img style="border-radius: 50%;" src="https://avatars2.githubusercontent.com/u/45704031?v=4" width="100px;" alt=""/></a><br /><a href="https://www.linkedin.com/in/enzoitaliano/" title="Enzo Italiano"><img href="https://www.linkedin.com/in/enzoitaliano/" src="https://img.shields.io/badge/-EnzoItaliano-0077B5?style=flat&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/enzoitaliano/"></a></td>
    <td align="center"><a href="https://www.linkedin.com/in/hmarcuzzo/"><img style="border-radius: 50%;" src="https://avatars2.githubusercontent.com/u/42159311?v=4" width="100px;" alt=""/></a><br /><a href="https://www.linkedin.com/in/hmarcuzzo/" title="Henrique Marcuzzo"><img href="https://www.linkedin.com/in/hmarcuzzo/" src="https://img.shields.io/badge/-HenriqueMarcuzzo-0077B5?style=flat&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/hmarcuzzo/"></a></td>
  </tr>
</table>
