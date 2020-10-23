## API Utilizando .Net C# com EF 

## Documentação criada com Swagger.



### Padrões de Projetos.

-   Utilizei o Repository para fazer o isolamento da camada de dados, trazendo assim um emcapsulamento na lógica de acesso aos dados.
-   Com a injeção de dependencia tentei diminuir o nivel de acoplamento do projeto, removendo uma dependencia desnecessaria.
-   Na propria interface apliquei o conceito de singleton para garantir a existencia de apenas uma unica instancia.

### Funcionalidades.

-   Adicionei mais duas opçoes de buscas além a busca padrão por ID, a busca por descrição e também uma busca por categoria, a qual lista todos os cursos disponiveis na categoria selecionada.

#### Pacotes Utilizados

```bash
-   Swashbuckle.AspNetCore -v 5.0.0
-   Microsoft.AspNetCore.Mvc.NewtonsoftJson -v 12.0.3
-   Microsoft.AspNetCore.StaticFiles -v 2.2.0
-   Microsoft.EntityFrameworkCore.SqlServer -v 3.1.9


```
