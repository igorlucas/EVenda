# Projeto EVenda
Este projeto consiste em uma demonstração da comunicação de serviços através de mensagens com o Azure Service Bus.

Será mostrado a troca de mensagens entre 2 serviços: VendaService e EstoqueService. O EstoqueService é reponsável por gerenciar o estoque de produtos de forma geral, para isso ele 
realiza as operações de adição de novos produtos, atualização e listagem. O VendaService é responsável por listar os produtos com quantidade positiva em estoque e por realizar a 
venda de um produto.

Para a troca de mensagens será utilizado o recurso de filas do Azure Service Bus, onde existirá 3 tipos de filas:
- ProdutoCriado
- ProdutoEditado
- ProdutoVendido

Sempre que um novo produto for adicionado no EstoqueService, ele deverá ser adicionado no VendaServicve também, para isso o EstoqueService envia uma mensagem para a fila ProdutoCriado 
do Service Bus e o VendaService irá "escutar" as mensagens que chegam nessa fila e processá-las para atualizar o seu banco de dados.

Da mesma forma, sempre que um produto for atualizado no EstoqueService, ele deverá ser atualizado no VendaService também, para isso o EstoqueService envia uma mensagem para a 
fila ProdutoEditado do Service Bus e o VendaService irá "escutar" as mensagens que chegam nessa fila e processá-las para atualizar seu banco de dados.

Sempre que um produto for vendido no VendaService diminuindo a sua quantidade, ele deverá ser atualizado no EstoqueService também, para isso o VendaService envia uma mensagem 
para a fila ProdutoVendido do Service Bus e o EstoqueService irá "escutar" as mensagens que chegam nessa fila e processá-las para atualizar seu banco de dados.

# Representação gráfica do projeto
![](https://s4.aconvert.com/convert/p3r68-cdx67/a53mj-3agcp.png)

# Como executar esse projeto
## Requisitos:
- Net Core 3.1
- Conta do Azure com o recurso Service Bus
- Criar as filas ProdutoCriado, ProdutoEditado, ProdutoVendido

## Configuração
Será necessário configurar a sua connectionString do Service Bus. Você pode inserir no arquivo appsettings.json:
```
"ConnectionStrings": {
    "ServiceBus": "<SuaServiceBusConnectioString>"
  }
```
## Endpoints
### EstoqueService
- GET:  api/produtos: (Listar os produtos)
- POST: api/produtos: (Criar um novo produto)
- PUT:  api/produtos/{id}: (Editar um produto)

### VendaService
- GET: api/vendas/produtos: (Listar os produtos com quantidade positiva)
- POST: api/vendas/produtos/{produtoId}: (Realizar a venda de um produto)
