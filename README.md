# Aspnet Microservices Example

Projeto baseado no conteudo abordado no curso Microservices Architecture and Implementation on .NET 5, que pode ser encontrado em: https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/

O projeto foi desde o inicio implementado em .net 6.

Este projeto utiliza como pano de fundo um e-commerce para implementar diversos microservicos percorrendo toda a experiencia de compra de uma loja virtual.

## Catalog.API
Servico responsavel por lidar com as informacoes de produto.
[x] DB: MongoDB
- IP: localhost:8000

## Basket.API
Armazenamento dos dados de carrinho de compra em Cache.
[x] Cache: Redis
- IP: localhost:8001

## Discount.API
Gerenciamento dos cupons de desconto.
[x] DB: Postgres
[x] DB Admin: PgAdmin
[x] ORM: Dapper
- IP: localhost:8002

## Discount.gRPC
- IP: localhost:8003

## Fila
[ ] RabbitMQ
[ ] MassTransit

[ ] Polly
[ ] Ocelot
[ ] ELK

## IPs:
Portainer - http://localhost:9000/
PgAdmin - http://localhost:5050/


## Melhorias
[ ]