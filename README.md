# Catálogo de cursos SESI Indústria

Aplicação demonstrativa para pesquisar e consultar ofertas de cursos do SESI Indústria. A solução usa dados fictícios, uma API ASP.NET Core, SQLite e uma interface responsiva baseada no protótipo Figma fornecido.

## Tecnologias

- .NET 10 / ASP.NET Core Minimal API
- SQLite, com o provedor `Microsoft.Data.Sqlite` 10.0.13
- HTML, CSS e JavaScript sem framework ou etapa de compilação
- Open Sans e imagens fornecidas na atividade

## Arquitetura

O navegador serve os arquivos estáticos em `wwwroot/` pela própria aplicação ASP.NET Core. A interface consulta a API REST no mesmo host; a API aplica pesquisa e filtros com comandos parametrizados e lê cursos e conteúdo programático do banco SQLite.

```text
Navegador
   ├── wwwroot/index.html, styles.css, app.js
   └── /api/courses ── ASP.NET Core ── SQLite
```

O banco e as tabelas são criados no primeiro início. Se a tabela de cursos estiver vazia, os seis cursos fictícios de `Data/CourseSeed.cs` e seus módulos/aulas são carregados automaticamente.

## Estrutura

```text
.
├── App_Data/                 Banco SQLite criado na execução
├── Data/
│   ├── CourseModels.cs       Modelos de resposta e carga inicial
│   ├── CourseRepository.cs   Criação, carga e consultas SQLite
│   └── CourseSeed.cs         Cursos fictícios, módulos e aulas
├── wwwroot/
│   ├── assets/               Imagens, ícones, logos e Open Sans
│   ├── app.js                Pesquisa, filtros e detalhes
│   ├── index.html            Página do catálogo
│   └── styles.css            Layout responsivo e estilos
├── CatalogoSesi.csproj
├── README.md
└── USO_DE_IA.md
```

## Requisitos e execução local

Instale o SDK .NET 10. Na pasta que contém `CatalogoSesi.csproj`, execute:

```powershell
dotnet restore
dotnet run
```

Abra o endereço local indicado pelo comando, normalmente `http://localhost:5000` ou `https://localhost:5001`. O banco é criado em `App_Data/courses.db`; não é preciso instalar um servidor de banco de dados.

Para compilar:

```powershell
dotnet build
```

Se o projeto estiver em uma pasta sincronizada e o build falhar ao gravar arquivos mapeados, use diretórios de build fora dela:

```powershell
$buildRoot = Join-Path $env:TEMP "CatalogoSesi-build"
$obj = Join-Path $buildRoot "obj"
$out = Join-Path $buildRoot "out"
New-Item -ItemType Directory -Force -Path $obj, $out | Out-Null

dotnet restore .\CatalogoSesi.csproj `
  "/p:BaseIntermediateOutputPath=$obj\" `
  "/p:MSBuildProjectExtensionsPath=$obj\" `
  "/p:OutputPath=$out\" `
  "/p:OutDir=$out\" `
  '/p:DefaultItemExcludes=obj\**'

dotnet build .\CatalogoSesi.csproj --no-restore `
  /p:UseAppHost=false `
  "/p:BaseIntermediateOutputPath=$obj\" `
  "/p:MSBuildProjectExtensionsPath=$obj\" `
  "/p:OutputPath=$out\" `
  "/p:OutDir=$out\" `
  '/p:DefaultItemExcludes=obj\**'
```

Para iniciar com uma porta conhecida:

```powershell
$env:ASPNETCORE_URLS = "http://localhost:5080"
dotnet run
```

O banco pode ser removido manualmente para reconstruir a carga inicial. Isso apaga alterações feitas diretamente nele.

## API

### `GET /api/courses`

Lista os cursos e aceita filtros combináveis, todos opcionais:

| Parâmetro | Exemplo | Comportamento |
| --- | --- | --- |
| `search` | `metalurgia` | Busca pelo nome completo ou breve |
| `category` | `Tecnologia` | Correspondência exata de categoria |
| `modality` | `Online` | Correspondência exata de modalidade |
| `status` | `Inscrições abertas` | Correspondência exata de status |

Exemplo:

```text
GET /api/courses?search=industrial&modality=Híbrido&status=Inscrições%20abertas
```

Retorna uma lista com nome, descrição, categoria, modalidade, carga horária, público-alvo, status, imagem e data de disponibilização. Uma lista vazia representa zero resultados.

### `GET /api/courses/{id}`

Retorna um curso e os respectivos módulos e descrições de aulas. IDs inexistentes retornam `404`.

## Modelo de dados

- **Courses**: dados públicos do curso, status e data de disponibilização.
- **Modules**: módulos ordenados de cada curso.
- **Lessons**: descrições de aulas ordenadas de cada módulo.

Relações: `Courses 1:N Modules 1:N Lessons`. As chaves estrangeiras são configuradas no esquema e os valores dos filtros são enviados como parâmetros SQL, não concatenados na consulta.

## Requisitos de catálogo implementados

Há seis cursos fictícios, cinco categorias, três modalidades e três status, com cargas horárias, públicos, módulos e descrições de aulas variados. A interface oferece pesquisa pelo nome, filtros combináveis, contagem, limpeza de filtros, detalhes do curso, estado de carregamento, estado vazio e mensagem de erro.

## Responsividade e acessibilidade

- Grade fluida: três colunas em desktop, duas em telas intermediárias e uma em celulares.
- Filtros se reorganizam em uma coluna em telas estreitas e navegação móvel abre por botão.
- HTML semântico, títulos hierárquicos, rótulos explícitos, link para pular conteúdo, estados anunciados com `aria-live`/`role=status` e foco visível.
- Imagens decorativas usam texto alternativo vazio; imagens e botões mantêm áreas utilizáveis em telas de toque.
- A preferência `prefers-reduced-motion` reduz animações e rolagem suave.

## Publicação

A aplicação é autocontida: publicar a pasta do projeto em uma hospedagem gratuita que ofereça execução de ASP.NET Core/.NET 10 e armazenamento persistente para `App_Data/courses.db`. Configure `ASPNETCORE_URLS` conforme a plataforma e mantenha a pasta de dados gravável. A disponibilidade de hospedagem gratuita para .NET e seu armazenamento persistente depende do provedor e de seus limites; confirme essas condições antes de publicar.

Não há link de repositório nem de aplicação publicada nesta entrega. O SDK .NET 10 foi instalado neste ambiente, o build foi validado sem avisos e os endpoints principais passaram em testes HTTP locais. A publicação ainda precisa ser executada em uma plataforma escolhida.

## Limitações conhecidas

- O banco é inicializado por criação de esquema e carga inicial, sem ferramenta formal de migrations.
- Não há gerenciamento de cursos, login, matrícula ou integração real com Moodle/AVA.
- A área do aluno e o ícone de reprodução do vídeo são elementos visuais demonstrativos.
- Os cursos e datas são fictícios; as imagens são compartilhadas entre alguns cartões.
- O protótipo Figma recebido serviu como direção visual; elementos das páginas de catálogo e detalhes foram adaptados para acomodar os filtros e os estados obrigatórios do desafio.
