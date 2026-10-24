# Limitações, itens não concluídos e diferenças em relação ao Figma

## Itens não concluídos ou fora do escopo

- **Integração com Moodle/AVA:** não realizada (diferencial opcional, conforme o documento).
- **Cadastro, edição e exclusão de cursos:** não desenvolvidos, pois não eram exigidos.
- **Login e "Área do aluno":** o botão "Área do aluno" abre em nova aba o login do portal https://portaleduca.sesisp.org.br; não há autenticação própria.
- **Migrations formais:** o esquema é criado no início da aplicação e o script equivalente está em `Database/courses.sql`.
- **Publicação e repositório:** repositório em https://github.com/vitorpilao/SESI-SENAI e aplicação em https://sesi-senai.onrender.com/. No plano gratuito do Render o serviço hiberna e a primeira requisição pode demorar.
- **Hospedagem gratuita:** pode hibernar após inatividade e o disco é temporário; o banco é recriado com a carga inicial a cada início.
- **Testes automatizados:** não foram criados; a validação foi feita por build e testes manuais/HTTP da API.
- **Validação em leitores de tela e em dispositivos reais:** não realizada.

## Diferenças em relação ao protótipo Figma e justificativas

| Ponto | Figma | Aplicação | Justificativa |
| --- | --- | --- | --- |
| Pesquisa e filtros | Não aparecem nas telas consultadas | Painel de busca e filtros acima da lista | Exigidos pelo documento (pesquisa, filtros, contagem, limpar). |
| Lista de cursos | Carrossel horizontal em "Principais cursos" | Grade responsiva de cartões | A grade comporta contagem, filtros e estado vazio e funciona melhor em telas pequenas. |
| Página de detalhes | Tela única com vídeo, início, certificação e benefícios | Mesma estrutura, com descrição, público-alvo e módulos/aulas | Campos obrigatórios do documento não presentes no protótipo. |
| Botão "Matricule-se" | Presente | "Conheça outros cursos" | Não há matrícula nem integração com Moodle. |
| Vídeo | Miniatura com botão de reprodução | Imagem do curso com ícone decorativo | Não há vídeo disponível nos materiais. |
| Fluxo "03. AVA" | Tela de ambiente virtual | Não implementado | Depende de integração com o Moodle, que é diferencial. |
| Datas | Início em 05/01/2026 | Datas fictícias futuras | A data do protótipo já passou. |
| Imagens | Imagens específicas por curso | Poucas imagens fornecidas, reutilizadas | Só há três imagens nos materiais. |
| Ajustes finos | Medidas e espaçamentos do Figma | Aproximados visualmente | O protótipo foi consultado por captura de tela, sem inspeção das medidas de design. |
