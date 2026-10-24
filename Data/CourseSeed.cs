namespace CatalogoSesi.Data;

public static class CourseSeed
{
    public static IReadOnlyList<SeedCourse> Courses { get; } =
    [
        new(
            "matematica-precisao",
            "Matemática para a Precisão: Medidas e Escalas na Metalurgia",
            "Matemática para a Precisão",
            "Aprenda a interpretar medidas, escalas e tolerâncias para tomar decisões mais precisas nos processos metalúrgicos.",
            "Matemática",
            "Presencial",
            40,
            "Estudantes e profissionais de produção, metrologia e processos metalúrgicos.",
            "Inscrições abertas",
            "/assets/video-thumb.png",
            "2026-11-05",
            "SESI",
            [
                new("Fundamentos da medição", "Conheça unidades, instrumentos e a leitura correta de medidas industriais.",
                    ["Sistema Internacional de Unidades", "Régua, paquímetro e micrômetro", "Leitura e registro de medidas"]),
                new("Escalas e tolerâncias", "Aplique escalas e tolerâncias dimensionais em situações de fabricação.",
                    ["Escalas em desenhos técnicos", "Tolerâncias dimensionais", "Exercícios aplicados à metalurgia"])
            ]),
        new(
            "eletricidade-industrial",
            "Eletricidade Industrial: Instalação e Manutenção",
            "Eletricidade Industrial",
            "Desenvolva conhecimentos para interpretar circuitos e executar rotinas seguras de manutenção elétrica industrial.",
            "Tecnologia",
            "Híbrido",
            60,
            "Eletricistas, auxiliares de manutenção e estudantes de cursos técnicos.",
            "Inscrições abertas",
            "/assets/hero-image.png",
            "2026-11-12",
            "SESI",
            [
                new("Circuitos e componentes", "Identifique os componentes e interprete diagramas elétricos industriais.",
                    ["Grandezas elétricas", "Leitura de diagramas", "Dispositivos de proteção"]),
                new("Manutenção segura", "Planeje inspeções e intervenções considerando boas práticas de segurança.",
                    ["Inspeção preventiva", "Procedimentos de bloqueio", "Registro de manutenção"])
            ]),
        new(
            "producao-enxuta",
            "Produção Enxuta: Melhoria Contínua na Indústria",
            "Produção Enxuta",
            "Explore ferramentas de melhoria contínua para identificar desperdícios e otimizar fluxos de trabalho.",
            "Gestão",
            "Online",
            24,
            "Lideranças, operadores e profissionais interessados em melhoria de processos.",
            "Em andamento",
            "/assets/banner-ava.png",
            "2026-10-01",
            "SESI",
            [
                new("Princípios enxutos", "Reconheça desperdícios e visualize o fluxo de valor.",
                    ["Valor para o cliente", "Os oito desperdícios", "Mapeamento do fluxo"]),
                new("Rotina de melhoria", "Estruture pequenas melhorias com participação das equipes.",
                    ["Ciclo de melhoria", "Gestão visual", "Indicadores de processo"])
            ]),
        new(
            "seguranca-trabalho",
            "Segurança do Trabalho em Ambientes Industriais",
            "Segurança do Trabalho",
            "Reconheça riscos do ambiente industrial e contribua para rotinas de trabalho mais seguras.",
            "Segurança",
            "Presencial",
            32,
            "Trabalhadores da indústria, membros de equipes operacionais e estudantes.",
            "Inscrições abertas",
            "/assets/video-thumb.png",
            "2026-11-20",
            "SESI",
            [
                new("Perigos e prevenção", "Identifique perigos e relacione medidas preventivas às atividades.",
                    ["Perigo e risco", "Equipamentos de proteção", "Análise preliminar de riscos"]),
                new("Resposta a emergências", "Conheça orientações básicas de prevenção e resposta.",
                    ["Sinalização e rotas", "Comunicação de incidentes", "Plano de emergência"])
            ]),
        new(
            "sustentabilidade-industrial",
            "Sustentabilidade e Eficiência de Recursos na Indústria",
            "Sustentabilidade Industrial",
            "Entenda como práticas responsáveis podem reduzir impactos e apoiar a eficiência na operação industrial.",
            "Sustentabilidade",
            "Online",
            20,
            "Profissionais de produção, qualidade, meio ambiente e estudantes.",
            "Em breve",
            "/assets/banner-ava.png",
            "2026-12-02",
            "SESI",
            [
                new("Indústria e meio ambiente", "Relacione processos produtivos a aspectos e impactos ambientais.",
                    ["Aspectos ambientais", "Uso eficiente de água", "Consumo de energia"]),
                new("Boas práticas", "Conheça ações para reduzir resíduos e acompanhar resultados.",
                    ["Segregação de resíduos", "Economia circular", "Indicadores de sustentabilidade"])
            ]),
        new(
            "logistica-digital",
            "Logística Digital e Controle de Estoques",
            "Logística Digital",
            "Aplique conceitos de organização de estoques e acompanhe operações com ferramentas digitais.",
            "Tecnologia",
            "Híbrido",
            36,
            "Auxiliares de logística, almoxarifes e profissionais de operações.",
            "Inscrições abertas",
            "/assets/hero-image.png",
            "2026-11-28",
            "SESI",
            [
                new("Organização de estoques", "Estruture processos de recebimento, armazenagem e inventário.",
                    ["Classificação de materiais", "Endereçamento", "Inventário rotativo"]),
                new("Informação logística", "Use indicadores e registros para apoiar decisões operacionais.",
                    ["Rastreabilidade", "Indicadores de estoque", "Sistemas de apoio"])
            ])
    ];
}
