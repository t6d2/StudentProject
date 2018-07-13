use StudentLife

insert into Subjects
(Description)
values
('Chimica'),
('Fisica'),
('Italiano'),
('Matematica'),
('Storia')

insert into ClassRoomTaskTypes
(Description)
values
('Compito'),
('Interrogazione'),
('Verifica')

insert into ClassRoomTasks
(WhenDate, Vote, TaskId, SubjectId)
values
('2018-02-02',5,2,1),
('2018-03-12',6,2,1),
('2018-04-20',null,1,1),
('2018-04-11',null,3,1),
('2018-03-19',7,1,2),
('2018-04-11',7,1,2),
('2018-04-20',6,3,2),
('2018-05-28',null,1,2),
('2018-01-05',5,2,3),
('2018-04-23',null,3,3),
('2018-04-26',null,1,3),
('2018-02-27',8,2,4),
('2018-03-21',9,1,4),
('2018-05-27',null,2,5)

insert into Homeworks
(Description, StartDate, EndDate, SubjectId)
values
('Studiare i nitrati e i carbonati per la verifica di aprile', '2018-04-03', '2018-04-03',1),
('Il 4 aprile pomeriggio ripassare con Andrea la lezione della mattina perchè so già che non capirò nulla', '2018-04-04', '2018-04-04', 1),
('Per il compito studiare la tabella degli elementi', '2018-04-01', null, 1),
('Rivedere primo principio Termodinamica per il compito', '2018-03-01', '2018-03-05', 2),
('Preparare Ricerca su Einstein per interrogazione', '2018-04-10', '2018-04-12', 2),
('Rivedere con Marco e Paola il romanticismo, non ho voglia di farlo da solo', '2018-04-02', '2018-04-06', 3),
('Probabile compito di Italiano sul Foscolo, ripassare', '2018-04-02', '2018-04-06', 3),
('Chiedere al prof di Mate di rispiegare eserczio sugli integrali' ,'2018-02-05', '2018-02-05',4),
('Fare i 2 esercizi sulle derivate' ,'2018-02-25', '2018-02-26',4),
('Finire il libro "Il sergente nella neve" prima della interrogazione di fine maggio' ,'2018-03-25', null, 5),
('Ricerca su Seconda Guerra Mondiale prima della Verifica' ,'2018-03-29', null, 5),
('Cercare in internet temi già svolti sul Foscolo', '2018-04-02', '2018-04-05',3),
('Chiedere a Luca le soluzioni delgi esercizi di matematica', '2018-02-27', '2018-02-27',4),
('Rivedere il terzo canto del Paradiso di Dante, mi interrogherà di sicuro' ,'2018-04-09', '2018-04-15', 3),
('Per la verifica di Mate di fine maggio, cercare esercizi sul web' ,'2018-04-15', '2018-04-25', 3)