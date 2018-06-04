create table Films 
(
 id int identity(1, 1) primary key,
 title nvarchar(100),
 trailer_url nvarchar(max),
 image_url nvarchar(max) 
)

create table Categories
(
  id int identity (1,1) primary key,
  title nvarchar(100)
)

create table FilmCategories
( 
 id int identity (1,1) primary key,
 id_film int foreign key references Films(id),
 id_category int foreign key references Categories(id)
)

create table [FilmDescription]
( 
 id int identity (1,1) primary key,
 id_film int foreign key references Films(id),
 content nvarchar(max)
) 

create table Cities
( 
 id int identity (1,1) primary key,
 title nvarchar(100)
)

create table Cinemas
(
 id int identity (1,1) primary key,
 title nvarchar(100),
 id_city int foreign key references Cities(id),
 [address] nvarchar(100)
)

create table MovieSession
(
 id int identity (1, 1) primary key,
 id_film int foreign key references Films(id),
 id_cinema int foreign key references Cinemas(id),
 [date] date
)

create table FreePlacesForSession 
(
 id int identity (1, 1) primary key, 
 id_session int foreign key references MovieSession(id),
 place_number int 
)

create table Users 
(
id int identity (1,1) primary key,
phone_number varchar(30),
[password] varchar(50),
email varchar(100) 
)

create table Tickets 
(
id int identity (1,1) primary key,
id_user int foreign key references Users(id),
id_session int foreign key references MovieSession(id)
)

create table Receipts 
(
id int identity (1,1) primary key,
id_ticket int foreign key references Tickets(id),
content nvarchar(max)
)

insert into Films (title, trailer_url, image_url)
values 
('Tomb Raider (2018)', 'https://www.youtube.com/watch?v=rOEHpkZCc1Y', 'https://resizing.flixster.com/QIx2px5QBi8MfeA-_xVJIgSRS5U=/206x305/v1.bTsxMjYwOTEwMDtqOzE3NjUwOzEyMDA7Mjc2NDs0MDk2'),
('Black Panther (2018)', 'https://youtu.be/xjDjIWPwcPU', 'https://cdn.flickeringmyth.com/wp-content/uploads/2017/11/Black-Panther-poster-206x300.jpg'),
('Ready Player One (2018)', 'https://youtu.be/cSp1dM2Vj48', 'https://ia.media-imdb.com/images/M/MV5BY2JiYTNmZTctYTQ1OC00YjU4LWEwMjYtZjkwY2Y5MDI0OTU3XkEyXkFqcGdeQXVyNTI4MzE4MDU@._V1_UX182_CR0,0,182,268_AL_.jpg')

insert into Categories (title)
values ('Action'), ('Adventure'), ('Sci-Fi')

insert into FilmCategories (id_film, id_category)
values (1, 1), (1, 2), (2, 1), (3, 1), (3, 3)

insert into [FilmDescription] (id_film, content)
values
(1, 'Tomb Raider is a 2018 action-adventure film directed by Roar Uthaug with a screenplay by Geneva Robertson-Dworet and Alastair Siddons, from a story by Evan Daugherty and Robertson-Dworet. It is based on the 2013 video game of the same name, with some elements of its sequel by Crystal Dynamics, and is a reboot of the Tomb Raider film series. The film stars Alicia Vikander as Lara Croft, in which she embarks on a perilous journey to her fathers last-known destination, hoping to solve the mystery of his disappearance. Dominic West, Walton Goggins, Daniel Wu, and Kristin Scott Thomas appear in supporting roles.'),
(2, 'Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the eighteenth film in the Marvel Cinematic Universe (MCU). The film is directed by Ryan Coogler, who co-wrote the screenplay with Joe Robert Cole, and stars Chadwick Boseman as T`Challa / Black Panther, alongside Michael B. Jordan, Lupita Nyong`o, Danai Gurira, Martin Freeman, Daniel Kaluuya, Letitia Wright, Winston Duke, Angela Bassett, Forest Whitaker, and Andy Serkis. In Black Panther, TChalla returns home as king of Wakanda but finds his sovereignty challenged by a new adversary, in a conflict with global consequences.'),
(3, 'Ready Player One is a 2018 American science fiction adventure film, produced and directed by Steven Spielberg, and written by Zak Penn and Ernest Cline, based on Cline`s 2011 novel of the same name. The film stars Tye Sheridan, Olivia Cooke, Ben Mendelsohn, Lena Waithe, Simon Pegg, and Mark Rylance.') 

insert into Cities (title)
values ('Kyiv'), ('Lviv'), ('Odesa')

