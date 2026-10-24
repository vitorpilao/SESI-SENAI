BEGIN TRANSACTION;
CREATE TABLE Courses (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    ShortName TEXT NOT NULL,
    Description TEXT NOT NULL,
    Category TEXT NOT NULL,
    Modality TEXT NOT NULL,
    Workload INTEGER NOT NULL,
    TargetAudience TEXT NOT NULL,
    Status TEXT NOT NULL,
    Image TEXT NOT NULL,
    AvailableDate TEXT NULL,
    Certification TEXT NOT NULL
);
INSERT INTO "Courses" VALUES('matematica-precisao','Matemática para a Precisão: Medidas e Escalas na Metalurgia','Matemática para a Precisão','Aprenda a interpretar medidas, escalas e tolerâncias para tomar decisões mais precisas nos processos metalúrgicos.','Matemática','Presencial',40,'Estudantes e profissionais de produção, metrologia e processos metalúrgicos.','Inscrições abertas','/assets/video-thumb.png','2026-11-05','SESI');
INSERT INTO "Courses" VALUES('eletricidade-industrial','Eletricidade Industrial: Instalação e Manutenção','Eletricidade Industrial','Desenvolva conhecimentos para interpretar circuitos e executar rotinas seguras de manutenção elétrica industrial.','Tecnologia','Híbrido',60,'Eletricistas, auxiliares de manutenção e estudantes de cursos técnicos.','Inscrições abertas','/assets/hero-image.png','2026-11-12','SESI');
INSERT INTO "Courses" VALUES('producao-enxuta','Produção Enxuta: Melhoria Contínua na Indústria','Produção Enxuta','Explore ferramentas de melhoria contínua para identificar desperdícios e otimizar fluxos de trabalho.','Gestão','Online',24,'Lideranças, operadores e profissionais interessados em melhoria de processos.','Em andamento','/assets/banner-ava.png','2026-10-01','SESI');
INSERT INTO "Courses" VALUES('seguranca-trabalho','Segurança do Trabalho em Ambientes Industriais','Segurança do Trabalho','Reconheça riscos do ambiente industrial e contribua para rotinas de trabalho mais seguras.','Segurança','Presencial',32,'Trabalhadores da indústria, membros de equipes operacionais e estudantes.','Inscrições abertas','/assets/video-thumb.png','2026-11-20','SESI');
INSERT INTO "Courses" VALUES('sustentabilidade-industrial','Sustentabilidade e Eficiência de Recursos na Indústria','Sustentabilidade Industrial','Entenda como práticas responsáveis podem reduzir impactos e apoiar a eficiência na operação industrial.','Sustentabilidade','Online',20,'Profissionais de produção, qualidade, meio ambiente e estudantes.','Em breve','/assets/banner-ava.png','2026-12-02','SESI');
INSERT INTO "Courses" VALUES('logistica-digital','Logística Digital e Controle de Estoques','Logística Digital','Aplique conceitos de organização de estoques e acompanhe operações com ferramentas digitais.','Tecnologia','Híbrido',36,'Auxiliares de logística, almoxarifes e profissionais de operações.','Inscrições abertas','/assets/hero-image.png','2026-11-28','SESI');
CREATE TABLE Lessons (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ModuleId INTEGER NOT NULL REFERENCES Modules(Id) ON DELETE CASCADE,
    Description TEXT NOT NULL,
    Position INTEGER NOT NULL
);
INSERT INTO "Lessons" VALUES(1,1,'Sistema Internacional de Unidades',0);
INSERT INTO "Lessons" VALUES(2,1,'Régua, paquímetro e micrômetro',1);
INSERT INTO "Lessons" VALUES(3,1,'Leitura e registro de medidas',2);
INSERT INTO "Lessons" VALUES(4,2,'Escalas em desenhos técnicos',0);
INSERT INTO "Lessons" VALUES(5,2,'Tolerâncias dimensionais',1);
INSERT INTO "Lessons" VALUES(6,2,'Exercícios aplicados à metalurgia',2);
INSERT INTO "Lessons" VALUES(7,3,'Grandezas elétricas',0);
INSERT INTO "Lessons" VALUES(8,3,'Leitura de diagramas',1);
INSERT INTO "Lessons" VALUES(9,3,'Dispositivos de proteção',2);
INSERT INTO "Lessons" VALUES(10,4,'Inspeção preventiva',0);
INSERT INTO "Lessons" VALUES(11,4,'Procedimentos de bloqueio',1);
INSERT INTO "Lessons" VALUES(12,4,'Registro de manutenção',2);
INSERT INTO "Lessons" VALUES(13,5,'Valor para o cliente',0);
INSERT INTO "Lessons" VALUES(14,5,'Os oito desperdícios',1);
INSERT INTO "Lessons" VALUES(15,5,'Mapeamento do fluxo',2);
INSERT INTO "Lessons" VALUES(16,6,'Ciclo de melhoria',0);
INSERT INTO "Lessons" VALUES(17,6,'Gestão visual',1);
INSERT INTO "Lessons" VALUES(18,6,'Indicadores de processo',2);
INSERT INTO "Lessons" VALUES(19,7,'Perigo e risco',0);
INSERT INTO "Lessons" VALUES(20,7,'Equipamentos de proteção',1);
INSERT INTO "Lessons" VALUES(21,7,'Análise preliminar de riscos',2);
INSERT INTO "Lessons" VALUES(22,8,'Sinalização e rotas',0);
INSERT INTO "Lessons" VALUES(23,8,'Comunicação de incidentes',1);
INSERT INTO "Lessons" VALUES(24,8,'Plano de emergência',2);
INSERT INTO "Lessons" VALUES(25,9,'Aspectos ambientais',0);
INSERT INTO "Lessons" VALUES(26,9,'Uso eficiente de água',1);
INSERT INTO "Lessons" VALUES(27,9,'Consumo de energia',2);
INSERT INTO "Lessons" VALUES(28,10,'Segregação de resíduos',0);
INSERT INTO "Lessons" VALUES(29,10,'Economia circular',1);
INSERT INTO "Lessons" VALUES(30,10,'Indicadores de sustentabilidade',2);
INSERT INTO "Lessons" VALUES(31,11,'Classificação de materiais',0);
INSERT INTO "Lessons" VALUES(32,11,'Endereçamento',1);
INSERT INTO "Lessons" VALUES(33,11,'Inventário rotativo',2);
INSERT INTO "Lessons" VALUES(34,12,'Rastreabilidade',0);
INSERT INTO "Lessons" VALUES(35,12,'Indicadores de estoque',1);
INSERT INTO "Lessons" VALUES(36,12,'Sistemas de apoio',2);
CREATE TABLE Modules (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CourseId TEXT NOT NULL REFERENCES Courses(Id) ON DELETE CASCADE,
    Name TEXT NOT NULL,
    Description TEXT NOT NULL,
    Position INTEGER NOT NULL
);
INSERT INTO "Modules" VALUES(1,'matematica-precisao','Fundamentos da medição','Conheça unidades, instrumentos e a leitura correta de medidas industriais.',0);
INSERT INTO "Modules" VALUES(2,'matematica-precisao','Escalas e tolerâncias','Aplique escalas e tolerâncias dimensionais em situações de fabricação.',1);
INSERT INTO "Modules" VALUES(3,'eletricidade-industrial','Circuitos e componentes','Identifique os componentes e interprete diagramas elétricos industriais.',0);
INSERT INTO "Modules" VALUES(4,'eletricidade-industrial','Manutenção segura','Planeje inspeções e intervenções considerando boas práticas de segurança.',1);
INSERT INTO "Modules" VALUES(5,'producao-enxuta','Princípios enxutos','Reconheça desperdícios e visualize o fluxo de valor.',0);
INSERT INTO "Modules" VALUES(6,'producao-enxuta','Rotina de melhoria','Estruture pequenas melhorias com participação das equipes.',1);
INSERT INTO "Modules" VALUES(7,'seguranca-trabalho','Perigos e prevenção','Identifique perigos e relacione medidas preventivas às atividades.',0);
INSERT INTO "Modules" VALUES(8,'seguranca-trabalho','Resposta a emergências','Conheça orientações básicas de prevenção e resposta.',1);
INSERT INTO "Modules" VALUES(9,'sustentabilidade-industrial','Indústria e meio ambiente','Relacione processos produtivos a aspectos e impactos ambientais.',0);
INSERT INTO "Modules" VALUES(10,'sustentabilidade-industrial','Boas práticas','Conheça ações para reduzir resíduos e acompanhar resultados.',1);
INSERT INTO "Modules" VALUES(11,'logistica-digital','Organização de estoques','Estruture processos de recebimento, armazenagem e inventário.',0);
INSERT INTO "Modules" VALUES(12,'logistica-digital','Informação logística','Use indicadores e registros para apoiar decisões operacionais.',1);
COMMIT;
