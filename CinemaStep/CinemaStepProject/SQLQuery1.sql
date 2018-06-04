create table olegks.Films 
(
 id int identity(1, 1) primary key,
 title nvarchar(100),
 trailer_url nvarchar(max),
 image_url nvarchar(max) 
)

create table olegks.Categories
(
  id int identity (1,1) primary key,
  title nvarchar(100)
)

create table olegks.FilmCategories
( 
 id int identity (1,1) primary key,
 id_film int foreign key references olegks.Films(id),
 id_category int foreign key references olegks.Categories(id)
)

create table olegks.[FilmDescription]
( 
 id int identity (1,1) primary key,
 id_film int foreign key references olegks.Films(id),
 content nvarchar(max)
) 

create table olegks.Cities
( 
 id int identity (1,1) primary key,
 title nvarchar(100)
)

create table olegks.Cinemas
(
 id int identity (1,1) primary key,
 title nvarchar(100),
 id_city int foreign key references olegks.Cities(id),
 [address] nvarchar(100)
)

create table olegks.Hall 
(
id int identity (1, 1) primary key,
title_number varchar(10)
)

ALTER TABLE olegks.Hall ADD id_cinema int foreign key references olegks.Cinemas(id)

select * from olegks.Hall
select * from olegks.Cinemas

update olegks.Hall
set id_cinema = 3
where id = 3

create table olegks.MovieSession
(
 id int identity (1, 1) primary key,
 id_film int foreign key references olegks.Films(id),
 id_cinema int foreign key references olegks.Cinemas(id),
 id_hall int foreign key references olegks.Hall(id),
 [date] date
)

create table olegks.FreePlacesForSession 
(
 id int identity (1, 1) primary key, 
 id_hall int foreign key references olegks.Hall(id),
 place_number int 
)

create table olegks.Users 
(
id int identity (1,1) primary key,
phone_number varchar(30),
[password] varchar(50),
email varchar(100) 
)

create table olegks.Tickets 
(
id int identity (1,1) primary key,
id_user int foreign key references olegks.Users(id),
id_session int foreign key references olegks.MovieSession(id)
)

create table olegks.Receipts 
(
id int identity (1,1) primary key,
id_ticket int foreign key references olegks.Tickets(id),
content nvarchar(max)
)

insert into olegks.Films (title, trailer_url, image_url)
values 
('Tomb Raider (2018)', 'https://www.youtube.com/watch?v=rOEHpkZCc1Y', 'https://resizing.flixster.com/QIx2px5QBi8MfeA-_xVJIgSRS5U=/206x305/v1.bTsxMjYwOTEwMDtqOzE3NjUwOzEyMDA7Mjc2NDs0MDk2'),
('Black Panther (2018)', 'https://youtu.be/xjDjIWPwcPU', 'https://cdn.flickeringmyth.com/wp-content/uploads/2017/11/Black-Panther-poster-206x300.jpg'),
('Ready Player One (2018)', 'https://youtu.be/cSp1dM2Vj48', 'https://ia.media-imdb.com/images/M/MV5BY2JiYTNmZTctYTQ1OC00YjU4LWEwMjYtZjkwY2Y5MDI0OTU3XkEyXkFqcGdeQXVyNTI4MzE4MDU@._V1_UX182_CR0,0,182,268_AL_.jpg')

insert into olegks.Categories (title)
values ('Action'), ('Adventure'), ('Sci-Fi')

insert into olegks.FilmCategories (id_film, id_category)
values (1, 1), (1, 2), (2, 1), (3, 1), (3, 3)

insert into olegks.[FilmDescription] (id_film, content)
values
(1, 'Tomb Raider is a 2018 action-adventure film directed by Roar Uthaug with a screenplay by Geneva Robertson-Dworet and Alastair Siddons, from a story by Evan Daugherty and Robertson-Dworet. It is based on the 2013 video game of the same name, with some elements of its sequel by Crystal Dynamics, and is a reboot of the Tomb Raider film series. The film stars Alicia Vikander as Lara Croft, in which she embarks on a perilous journey to her fathers last-known destination, hoping to solve the mystery of his disappearance. Dominic West, Walton Goggins, Daniel Wu, and Kristin Scott Thomas appear in supporting roles.'),
(2, 'Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the eighteenth film in the Marvel Cinematic Universe (MCU). The film is directed by Ryan Coogler, who co-wrote the screenplay with Joe Robert Cole, and stars Chadwick Boseman as T`Challa / Black Panther, alongside Michael B. Jordan, Lupita Nyong`o, Danai Gurira, Martin Freeman, Daniel Kaluuya, Letitia Wright, Winston Duke, Angela Bassett, Forest Whitaker, and Andy Serkis. In Black Panther, TChalla returns home as king of Wakanda but finds his sovereignty challenged by a new adversary, in a conflict with global consequences.'),
(3, 'Ready Player One is a 2018 American science fiction adventure film, produced and directed by Steven Spielberg, and written by Zak Penn and Ernest Cline, based on Cline`s 2011 novel of the same name. The film stars Tye Sheridan, Olivia Cooke, Ben Mendelsohn, Lena Waithe, Simon Pegg, and Mark Rylance.') 

insert into olegks.Cities (title)
values ('Kyiv'), ('Lviv'), ('Odesa')

insert into olegks.Cinemas ( title, id_city, [address] )
values ( 'Multiplex', 1, 'проспект Генерала Ватутіна, 2Т, ТРЦ SkyMall, Kyiv, 02000'),
('Cinema City', 3, 'Semafornyi Ln, 4, Odesa, Odessa Oblast, 65000'),
('Planet Cinema', 2, 'Pid Dubom Street, 7Б, Lviv, Lviv Oblast, 79000')

insert into olegks.Hall(title_number)
values ('1'), ('2'), ('3')

insert into olegks.MovieSession (id_film, id_cinema, id_hall, [date])
values (1, 1, '1', CURRENT_TIMESTAMP),
		(2, 1, '1', CURRENT_TIMESTAMP),
		 (3, 1, '1', CURRENT_TIMESTAMP),
	(1, 2, '2', CURRENT_TIMESTAMP),
	(2, 2, '2', CURRENT_TIMESTAMP),
	  (3, 3, '3', CURRENT_TIMESTAMP) 

insert into olegks.Users ( phone_number, [password], [email] )

values ( 'user', 'user', 'radchenko.ov00@gmail.com'), ('admin', 'admin', 'radchenko.ov00@gmail.com')

select * from olegks.Users
