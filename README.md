# Aspnet Microservices Example

Projeto baseado no conteudo abordado no curso Microservices Architecture and Implementation on .NET 5, que pode ser encontrado em: https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/

O projeto foi desde o inicio implementado em .net 6.
Este projeto utiliza como pano de fundo um e-commerce para implementar diversos microservicos percorrendo toda a experiencia de compra de uma loja virtual.
Os servicos foram conteinerizados utilizando docker-compose

## Catalog.API
Servico responsavel por lidar com as informacoes de produto.
[x] DB: MongoDB
- IP: localhost:8000

## Basket.API
Armazenamento dos dados de carrinho de compra em Cache.
- [x] Cache: Redis
- [x] Consumir serviço gRPC Discount
- [x] Publicar na fila utilizando MassTransit e RabbitMQ
- IP: localhost:8001

## Discount.API
Gerenciamento dos cupons de desconto.
- [x] DB: Postgres
- [x] DB Admin: PgAdmin
- IP: localhost:8002

## Discount.gRPC
- [x] ORM: Dapper
- [x] DB: Postgres
- [x] DB Admin: PgAdmin
- [x] Criação de mensagens Protobuf
- IP: localhost:8003

## Ordering.API
- [x] CQRS utilizando MediatR, FluentValidation e AutoMapper
- [x] ORM: Entity Framework
- [x] DB: SQLServer
- [x] Consumir da fila do RabbitMQ

## Fila
- [x] RabbitMQ
- [x] MassTransit
- [ ] CloudAMQP

## Api Gateway
- [x] Ocelot
- [x] RateLimit
- [x] Caching

## BFF
- [x] Shopping.Aggregator - Consumindo Basket, Ordering e Catalog
- [x] Polly

## Observability
- [x] ElasticSearch
- [x] Kibana
- [x] Serilog
- [x] HealthChecks
- [x] Watchdog




## IPs:
Portainer - http://localhost:9000/
PgAdmin - http://localhost:5050/
ApiGateway: http://localhost:8010
FrontEnd: http://localhost:8006
Kibana: http://localhost:5601/app/home
Watchdog: http://localhost:8007/

## Melhorias
[ ]