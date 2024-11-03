# TodoManager

## Tecnologias
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![SqLite](https://img.shields.io/badge/SqLite-Database-blue?logo=sqlite&style=for-the-badge&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-MicroORM-blue?logo=nuget&style=for-the-badge&logoColor=white)

## Badges
[![Processo de Build/Test](https://github.com/carloscampos2014/todomanager/actions/workflows/ci.yml/badge.svg)](https://github.com/carloscampos2014/todomanager/actions/workflows/ci.yml)

[![Publicação Relatório de Cobertura](https://github.com/carloscampos2014/todomanager/actions/workflows/pages/pages-build-deployment/badge.svg)](https://github.com/carloscampos2014/todomanager/actions/workflows/pages/pages-build-deployment)

## Relatório de Cobertura
O relatório de cobertura dos testes está disponível no GitHub Pages. Você pode acessá-lo clicando nos links abaixo:

- [Relatório de Cobertura do TodoManager.Domain](https://carloscampos2014.github.io/todomanager/domain-coverage)
- [Relatório de Cobertura do TodoManager.Database.Sqlite](https://carloscampos2014.github.io/todomanager/database-coverage/)

## Objetivo
API para CRUD de Lista de Tarefas

## Instruções para Rodar Testes
- Criar arquivo .env configurando seus dados baseados .model-env 

### Modelo de arquivo .env
```
DB_NAME=[nomebanco]
```

## URL Api
- `POST /api/todos` - Adiciona uma nova tarefa
  Exemplo de uso:
	```
	curl -X POST http://localhost:5000/api/todos \
	-H "Content-Type: application/json" \
	-d '{
		"name": "Estudar para a prova",
		"description": "Revisar os tópicos de C# e ASP.NET",
		"deadline": "2024-12-01T00:00:00Z",
		"priority": 1,
		"status": 0
	}'
	```
- `DELETE /api/todos/{id}` - Exclui uma tarefa
  Exemplo de uso:
	```
	curl -X DELETE http://localhost:5000/api/todos/{id}
	```
- `GET /api/todos` - Recupera todas as tarefas
  Exemplo de uso:
	```
    curl -X GET http://localhost:5000/api/todos
	```
- `GET /api/todos/{id}` - Recupera uma tarefa pelo ID
  Exemplo de uso:
	```
	curl -X GET http://localhost:5000/api/todos/{id}
	```
- `PUT /api/todos/{id}` - Altera uma tarefa
  Exemplo de uso:
	```
	curl -X PUT http://localhost:5000/api/todos/{id} \
	-H "Content-Type: application/json" \
	-d '{
		"name": "Estudar para a prova",
		"description": "Revisar todos os tópicos",
		"deadline": "2024-12-01T00:00:00Z",
		"priority": 1,
		"status": 1
	}'
	```
