﻿using CinemaPortal_ASP.NET_Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.Models
{
    public static class CinemaDbInitializer
    {
        public static void Initialize(CinemaDbContext context,string AppRootPath)
        {
            if (!context.CinemaCollection.Any())
            {
                context.CinemaCollection.AddRange(
                    new Cinema
                    {
                        Name = "Аватар",
                        Year = 2009,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Avatar.jpg", AppRootPath),
                        FilmMaker = "Джеймс Кэмерон",
                        Description = "Американский научно-фантастический фильм. Действие фильма происходит в 2154 году, когда человечество добывает ценный минерал анобтаниум на Пандоре, обитаемом спутнике газовой планеты в звёздной системе Альфы Центавра.",
                        UserName = "Allison Brown"
                    },
                    new Cinema
                    {
                        Name = "Такси",
                        Year = 1998,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Taxi.jpg", AppRootPath),
                        FilmMaker = "Жерар Пирес",
                        Description = "Французский кинофильм, сочетающий в себе элементы кинокомедии, боевика, приключенческого фильма и детектива. Действие фильма происходит в Марселе. Молодой араб Даниэль бросает работу доставщика пиццы ради того, чтобы осуществить свою давнюю мечту — стать водителем такси — Peugeot 406, нестандартно укомплектованного и способного развивать бешеную скорость. Проведя в новом амплуа всего пару дней, Даниэль становится местной знаменитостью.",
                        UserName = "Roger Lengel"
                    },
                    new Cinema
                    {
                        Name = "Титаник",
                        Year = 1997,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Titanic.jpg", AppRootPath),
                        FilmMaker = "Джеймс Кэмерон",
                        Description = "Американский фильм-катастрофа, в котором показана гибель легендарного лайнера «Титаник». Герои фильма, будучи членами различных социальных слоёв, влюбились друг в друга на борту лайнера, совершавшего свой первый и последний рейс через Атлантический океан в 1912 году.",
                        UserName = "Allison Brown"
                    },
                    new Cinema
                    {
                        Name = "Брат",
                        Year = 1997,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Брат.jpg", AppRootPath),
                        FilmMaker = "Алексей Балабанов",
                        Description = "Демобилизованный из армии Данила Багров возвращается в родной город. Скучная, однообразная жизнь провинции не может устроить крепкого русского парня, и он решает поехать в Питер, испытать себя. Тем более, что там, по слухам уже давно процветает его старший брат. Но «новая русская» жизнь северной столицы оказывается слишком неожиданной, а родной брат Данилы зарабатывает на жизнь заказными убийствами. Даниле предстоит многое узнать… и со многими разобраться.",
                        UserName = "Roger Lengel"
                    },
                    new Cinema
                    {
                        Name = "Исходный код",
                        Year = 2011,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Source_Code.jpg", AppRootPath),
                        FilmMaker = "Данкан Джонс",
                        Description = "Капитан Колтер Стивенс приходит в себя в Чикаго в теле человека по имени Шон Фентресс внутри поезда, где он знакомится с девушкой по имени Кристина. Прежде чем он может понять, что происходит, чудовищный взрыв разрушает поезд.Стивенс пробуждается внутри капсулы, где его через экран компьютера приветствует женщина в военной форме, Коллин Гудвин, и говорит Стивенсу, что он находится внутри «Исходного кода» — программы, которая позволяет вселяться в тело некоего человека в последние восемь минут его жизни.Ранее в тот же день бомба взорвалась и уничтожила поезд.Миссия Стивенса — найти бомбу и узнать, кто создал её, прежде чем ещё одна бомба, на этот раз грязная бомба, взорвётся где - то в Чикаго, что может привести к гибели миллионов людей.",
                        UserName = "Jim Corbin"
                    },
                    new Cinema
                    {
                        Name = "Кто там",
                        Year = 2015,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Кто_там.jpg", AppRootPath),
                        FilmMaker = "Элай Рот",
                        Description = "Семья архитектора Эвана (Киану Ривз) уезжает на отдых, а он остаётся доделывать срочный проект. Поздно вечером в дверь его дома стучатся две промокшие молодые девушки — Дженезис (Лоренца Иззо) и Бель (Ана де Армас). Эван вызывает им такси, они просят высушить их одежду, согреться и принять душ. Девушки соблазняют Эвана и он занимается с ними сексом. Утром девушки начинают хулиганить и мусорить в доме и угрожают всё рассказать его семье и полиции, так как они, по их словам, несовершеннолетние.",
                        UserName = "Bernard Duerr"
                    },
                    new Cinema
                    {
                        Name = "Волк с Уолл-стрит",
                        Year = 2013,
                        ImageMimeType = "image/jpg",
                        Poster = PicturesConvertor.GetFileBytes("\\Images\\Волк_с_Уолл-стрит.jpg", AppRootPath),
                        FilmMaker = "Мартин Скорсезе",
                        Description = "Повествование фильма начинается в 1987 году. Джордан Белфорт (Леонардо Ди Каприо) становится брокером в успешном инвестиционном банке L.F. Rothschild[en]. Его босс, Марк Ханна (Мэттью Макконахи), советует ему начинать распутный образ жизни, постоянно занимаясь онанизмом, дабы «заставить кровь бегать быстрее», и принимать кокаин. Вскоре банк закрывается после внезапного обвала индекса Доу — Джонса.",
                        UserName = "Allison Brown"
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}
