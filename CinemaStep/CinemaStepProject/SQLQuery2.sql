
select * from olegks.Users

alter table olegks.FreePlacesForSession
add is_empty bit

select * from olegks.FreePlacesForSession

select * from olegks.Hall

insert into olegks.FreePlacesForSession (id_hall, place_number, is_empty)
values (1, 1, 1),
		(1, 2, 1),
		(1, 3, 1),
		(1, 4, 1),
		(1, 5, 1),
		(2, 1, 1),
		(2, 2, 1),
		(2, 3, 1),
		(2, 4, 1),
		(2, 5, 1),
		(3, 1, 1),
		(3, 2, 1),
		(3, 3, 1),
		(3, 4, 1),
		(3, 5, 1)


select * from olegks.Films

insert into olegks.Films (title, trailer_url, image_url)
values ('Pacific Rim: Uprising (2018)', 'https://youtu.be/8BAhwgjMvnM',
 'https://ia.media-imdb.com/images/M/MV5BMjI3Nzg0MTM5NF5BMl5BanBnXkFtZTgwOTE2MTgwNTM@._V1_UX182_CR0,0,182,268_AL_.jpg')
 , ('Game Over, Man! (2018)', 'https://youtu.be/u7ZHi_dDSnQ', 
 'https://ia.media-imdb.com/images/M/MV5BMTUxNjI4MDU2OF5BMl5BanBnXkFtZTgwMDAzMzA1NDM@._V1_UX182_CR0,0,182,268_AL_.jpg')
 , ('The Greatest Showman (2017)', 'https://youtu.be/4zOeiLhcgPo',
  'https://ia.media-imdb.com/images/M/MV5BYjQ0ZWJkYjMtYjJmYS00MjJiLTg3NTYtMmIzN2E2Y2YwZmUyXkEyXkFqcGdeQXVyNjk5NDA3OTk@._V1_UX182_CR0,0,182,268_AL_.jpg')

insert into olegks.FilmCategories (id_film, id_category)
values (4, 1), (4, 2), (5, 2), (6, 1)

select * from olegks.MovieSession

update olegks.MovieSession
set [date] = CURRENT_TIMESTAMP

alter table olegks.MovieSession
alter column [date] date 

select * from olegks.Films
select * from olegks.FilmDescription