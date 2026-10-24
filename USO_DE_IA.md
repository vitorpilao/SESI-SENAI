# Uso de Inteligência Artificial

## Ferramenta

Copilot SDK no VS Code, utilizado como assistente de desenvolvimento.

## Objetivo

Usado para analisar as instruções da atividade, consultar a referência Figma compartilhada, estruturar a aplicação ASP.NET Core com SQLite e produzir a primeira versão do catálogo responsivo, da API e da documentação.

## Prompts relevantes

- “Preciso criar essa plataforma conforme descrito nas regras de negócio do arquivo anexo, poderia me ajudar?”
- “Ajudar a implementar o catálogo de cursos solicitado, com API, filtros combináveis, detalhes e documentação.”

## O que foi aproveitado ou descartado

Foram aproveitados os requisitos do PDF (campos do curso, quantidade mínima de dados, API, filtros, estados de interface e documentação), os materiais fornecidos e os elementos visuais observados no protótipo: marca SESI Indústria, hero vermelho com imagem industrial, navegação, cards de cursos e benefícios.

Não foram incluídas integração Moodle/AVA, matrícula ou gerenciamento administrativo, pois não são necessários para os requisitos obrigatórios e não foram fornecidas credenciais ou serviços para isso.

## Validação

Os requisitos foram conferidos com o texto extraído do PDF e as telas Landing page e Detalhes curso foram consultadas no link Figma fornecido. A carga inicial foi composta para atender às quantidades mínimas de cursos, categorias, modalidades, status, módulos e públicos. Depois de instalar o SDK .NET 10.0.402, o projeto foi restaurado e compilado sem avisos. Testes HTTP locais confirmaram a página, os seis cursos, filtro combinado, detalhes com módulos/aulas, resposta 404 para curso inexistente e carregamento de imagem.

## Exemplo de inadequação identificada e correção

O primeiro pacote de dados escolhido para SQLite (Microsoft.Data.Sqlite 10.0.0) trouxe uma dependência nativa SQLitePCLRaw.lib.e_sqlite3 2.1.11, para a qual o restore reportou um alerta de segurança alto. A dependência foi verificada contra o advisory GHSA-2m69-gcr7-jv3q; o provedor foi atualizado para Microsoft.Data.Sqlite 10.0.13, que utiliza SQLitePCLRaw 2.1.13. A restauração seguinte não reportou o alerta.

O detalhe mostrado no protótipo usa a data `05/01/2026`, que já está no passado na data desta implementação. Em vez de reutilizar essa informação como calendário vigente, foram usadas datas fictícias posteriores nos cursos que estão com inscrições abertas. O título de referência foi mantido no catálogo para preservar a intenção visual do protótipo.
