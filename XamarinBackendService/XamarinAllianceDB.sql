CREATE TABLE [dbo].[Characters] (
    [Id]          NVARCHAR (128)     DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (MAX)     NULL,
    [Biography]   NVARCHAR (MAX)     NULL,
    [DatabankUrl] NVARCHAR (MAX)     NULL,
    [ImageUrl]    NVARCHAR (MAX)     NULL,
    [Gender]      NVARCHAR (MAX)     NULL,
    [Height]      REAL               NOT NULL,
    [RemoteId]    NVARCHAR (MAX)     NULL,
    [Version]     ROWVERSION         NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysutcdatetime()) NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    CONSTRAINT [PK_dbo.Characters] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


CREATE CLUSTERED INDEX [IX_CreatedAt]
    ON [dbo].[Characters]([CreatedAt] ASC);


CREATE TABLE [dbo].[Movies] (
    [Id]          NVARCHAR (128)     DEFAULT (newid()) NOT NULL,
    [Title]       NVARCHAR (MAX)     NULL,
    [Description] NVARCHAR (MAX)     NULL,
    [RemoteId]    NVARCHAR (MAX)     NULL,
    [Version]     ROWVERSION         NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysutcdatetime()) NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    CONSTRAINT [PK_dbo.Movies] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


CREATE CLUSTERED INDEX [IX_CreatedAt]
    ON [dbo].[Movies]([CreatedAt] ASC);


CREATE TABLE [dbo].[Weapons] (
    [Id]          NVARCHAR (128)     DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (MAX)     NULL,
    [Description] NVARCHAR (MAX)     NULL,
    [RemoteId]    NVARCHAR (MAX)     NULL,
    [Version]     ROWVERSION         NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) DEFAULT (sysutcdatetime()) NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    CONSTRAINT [PK_dbo.Weapons] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


CREATE CLUSTERED INDEX [IX_CreatedAt]
    ON [dbo].[Weapons]([CreatedAt] ASC);


CREATE TABLE [dbo].[MovieCharacters] (
    [Movie_Id]     NVARCHAR (128) NOT NULL,
    [Character_Id] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.MovieCharacters] PRIMARY KEY CLUSTERED ([Movie_Id] ASC, [Character_Id] ASC),
    CONSTRAINT [FK_dbo.MovieCharacters_dbo.Movies_Movie_Id] FOREIGN KEY ([Movie_Id]) REFERENCES [dbo].[Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MovieCharacters_dbo.Characters_Character_Id] FOREIGN KEY ([Character_Id]) REFERENCES [dbo].[Characters] ([Id]) ON DELETE CASCADE
);


CREATE NONCLUSTERED INDEX [IX_Movie_Id]
    ON [dbo].[MovieCharacters]([Movie_Id] ASC);


CREATE NONCLUSTERED INDEX [IX_Character_Id]
    ON [dbo].[MovieCharacters]([Character_Id] ASC);



CREATE TABLE [dbo].[WeaponCharacters] (
    [Weapon_Id]    NVARCHAR (128) NOT NULL,
    [Character_Id] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.WeaponCharacters] PRIMARY KEY CLUSTERED ([Weapon_Id] ASC, [Character_Id] ASC),
    CONSTRAINT [FK_dbo.WeaponCharacters_dbo.Weapons_Weapon_Id] FOREIGN KEY ([Weapon_Id]) REFERENCES [dbo].[Weapons] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.WeaponCharacters_dbo.Characters_Character_Id] FOREIGN KEY ([Character_Id]) REFERENCES [dbo].[Characters] ([Id]) ON DELETE CASCADE
);


CREATE NONCLUSTERED INDEX [IX_Weapon_Id]
    ON [dbo].[WeaponCharacters]([Weapon_Id] ASC);


CREATE NONCLUSTERED INDEX [IX_Character_Id]
    ON [dbo].[WeaponCharacters]([Character_Id] ASC);



INSERT INTO [dbo].[Characters] ([Id], [Name], [Biography], [DatabankUrl], [ImageUrl], [Gender], [Height], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'1', N'Bail Organa', N'Bail Organa, a member of Alderaan''s royal family and the planet''s representative to the Republic Senate, earned respect on Coruscant for his honesty and his commitment to justice. Prior to the Clone Wars, Bail served alongside Senator Padmé Amidala as a member of Chancellor Palpatine''s Loyalist Committee. The two became close friends, and recruited like-minded allies to prevent Palpatine''s alarming grabs for power. At the end of the war, Bail Organa adopted Padmé''s daughter Leia and raised her as an Alderaanian princess. While representing Alderaan in the Imperial Senate, he worked secretly with allies such as Mon Mothma and Ahsoka Tano to assist rebel cells scattered across the galaxy, working towards the day that a unified Rebellion could emerge. Bail died when the Death Star destroyed his homeworld.', N'http://www.starwars.com/databank/bail-organa', N'https://lumiere-a.akamaihd.net/v1/images/Bail_Organa_e28bc2fc.jpeg', N'Male', 199, NULL, N'2/28/2017 2:30:51 PM +00:00', N'2/28/2017 2:30:51 PM +00:00', 0)
INSERT INTO [dbo].[Characters] ([Id], [Name], [Biography], [DatabankUrl], [ImageUrl], [Gender], [Height], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'2', N'Mon Mothma', N'A leader of the Galactic Senate''s Loyalist faction, Mon Mothma opposed Supreme Chancellor Palpatine''s policies during the final days of the Repulic. Working in secret, she helped found the Rebel Alliance, and served as its civilian leader during the long struggle against the Empire.', N'http://www.starwars.com/databank/mon-mothma', N'https://lumiere-a.akamaihd.net/v1/images/databank_monmothma_01_169_b62808e8.jpeg', N'Female', 150, NULL, N'2/28/2017 2:31:39 PM +00:00', N'2/28/2017 2:31:39 PM +00:00', 0)
INSERT INTO [dbo].[Characters] ([Id], [Name], [Biography], [DatabankUrl], [ImageUrl], [Gender], [Height], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'3', N'Padmé Amidala', N'Padmé Amidala was a courageous, hopeful leader, serving as Queen and then Senator of Naboo -- and was also handy with a blaster. Despite her ideals and all she did for the cause of peace, her secret, forbidden marriage to Jedi Anakin Skywalker would prove to have dire consequences for the galaxy.', N'http://www.starwars.com/databank/padme-amidala', N'https://lumiere-a.akamaihd.net/v1/images/Padme-Amidala_05d50c8a.jpeg', N'Female', 165, NULL, N'2/28/2017 2:32:18 PM +00:00', N'2/28/2017 2:32:18 PM +00:00', 0)
INSERT INTO [dbo].[Characters] ([Id], [Name], [Biography], [DatabankUrl], [ImageUrl], [Gender], [Height], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'4', N'Ahsoka Tano', N'Ahsoka Tano, a Togruta female, was the Padawan learner to Anakin Skywalker and a hero of the Clone Wars. Alongside Anakin, she grew from headstrong student into a mature leader. But her destiny laid along a different path than the Jedi.', N'http://www.starwars.com/databank/ahsoka-tano', N'https://lumiere-a.akamaihd.net/v1/images/Ahsoka-Tano_aa51fe5c.jpeg', N'Female', 170, NULL, N'2/28/2017 2:32:55 PM +00:00', N'2/28/2017 2:32:55 PM +00:00', 0)

INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'1', N'Star Wars: Episode II Attack of the Clones', N'Ten years after the invasion of Naboo, the galaxy is on the brink of civil war. Under the leadership of a renegade Jedi named Count Dooku, thousands of solar systems threaten to break away from the Galactic Republic. When an assassination attempt is made on Senator Padmé Amidala, the former Queen of Naboo, twenty-year-old Jedi apprentice Anakin Skywalker is assigned to protect her. In the course of his mission, Anakin discovers his love for Padmé as well as his own darker side. Soon, Anakin, Padmé, and Obi-Wan Kenobi are drawn into the heart of the Separatist movement and the beginning of the Clone Wars.', NULL, N'2/28/2017 2:34:08 PM +00:00', N'2/28/2017 2:34:08 PM +00:00', 0)
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'2', N'Star Wars: The Clone Wars', N'Star Wars: The Clone Wars follows the Republic’s clone army in their struggle against Count Dooku’s Separatist forces, and features fan-favorite characters like Anakin Skywalker, Obi-Wan Kenobi, Ahsoka Tano, and Darth Maul.', NULL, N'2/28/2017 2:34:25 PM +00:00', N'2/28/2017 2:34:25 PM +00:00', 0)
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'3', N'Star Wars: Episode III Revenge of the Sith', N'Years after the onset of the Clone Wars, the noble Jedi Knights lead a massive clone army into a galaxy-wide battle against the Separatists. When the sinister Sith unveil a thousand-year-old plot to rule the galaxy, the Republic crumbles and from its ashes rises the evil Galactic Empire. Jedi hero Anakin Skywalker is seduced by the dark side of the Force to become the Emperor''s new apprentice -- Darth Vader. The Jedi are decimated, as Obi-Wan Kenobi and Jedi Master Yoda are forced into hiding. The only hope for the galaxy are Anakin''s own offspring -- the twin children born in secrecy who will grow up to become heroes.', NULL, N'2/28/2017 2:34:44 PM +00:00', N'2/28/2017 2:34:44 PM +00:00', 0)
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'4', N'Rogue One: A Star Wars Story', N'From Lucasfilm comes the first of the Star Wars standalone films, “Rogue One: A Star Wars Story,” an all-new epic adventure. In a time of conflict, a group of unlikely heroes band together on a mission to steal the plans to the Death Star, the Empire’s ultimate weapon of destruction. This key event in the Star Wars timeline brings together ordinary people who choose to do extraordinary things, and in doing so, become part of something greater than themselves.', NULL, N'2/28/2017 2:35:13 PM +00:00', N'2/28/2017 2:35:13 PM +00:00', 0)
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'5', N'Star Wars: Episode VI Return of the Jedi', N'In the epic conclusion of the saga, the Empire prepares to crush the Rebellion with a more powerful Death Star while the Rebel fleet mounts a massive attack on the space station. Luke Skywalker confronts his father Darth Vader in a final climactic duel before the evil Emperor. In the last second, Vader makes a momentous choice: he destroys the Emperor and saves his son. The Empire is finally defeated, the Sith are destroyed, and Anakin Skywalker is thus redeemed. At long last, freedom is restored to the galaxy.', NULL, N'2/28/2017 2:35:37 PM +00:00', N'2/28/2017 2:35:37 PM +00:00', 0)
INSERT INTO [dbo].[Movies] ([Id], [Title], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'6', N'Star Wars: Episode I The Phantom Menace', N'Stranded on the desert planet Tatooine after rescuing young Queen Amidala from the impending invasion of Naboo, Jedi apprentice Obi-Wan Kenobi and his Jedi Master Qui-Gon Jinn discover nine-year-old Anakin Skywalker, a young slave unusually strong in the Force. Anakin wins a thrilling Podrace and with it his freedom as he leaves his home to be trained as a Jedi. The heroes return to Naboo where Anakin and the Queen face massive invasion forces while the two Jedi contend with a deadly foe named Darth Maul. Only then do they realize the invasion is merely the first step in a sinister scheme by the re-emergent forces of darkness known as the Sith.', NULL, N'2/28/2017 2:36:08 PM +00:00', N'2/28/2017 2:36:08 PM +00:00', 0)

INSERT INTO [dbo].[Weapons] ([Id], [Name], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'1', N'Blaster Pistol', N'The standard ranged weapon of both military personnel and civilians in the galaxy, the blaster pistol fires cohesive bursts of light-based energy called bolts. Appearing in a variety of shapes and sizes and sometimes augmented for other functions, including acension guns that fire grappling hooks, blaster pistols deliver a wide range of damage capability. Many blaster pistols have stun settings that incapacitate a target, rather than inflicting physical damage. While blasters do deliver a searing concussive blast, they can be foiled by magnetic seals and deflector shields.', NULL, N'2/28/2017 2:37:03 PM +00:00', N'2/28/2017 2:37:03 PM +00:00', 0)
INSERT INTO [dbo].[Weapons] ([Id], [Name], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'2', N'Lightsaber', N'The lightsaber is the weapon of a Jedi, an elegant weapon of a more civilized age. It can be used to cut through blast doors or enemies alike. Using the Force, a Jedi can predict and deflect incoming blaster bolts, and reflect them.', NULL, N'2/28/2017 2:37:26 PM +00:00', N'2/28/2017 2:37:26 PM +00:00', 0)
INSERT INTO [dbo].[Weapons] ([Id], [Name], [Description], [RemoteId], [CreatedAt], [UpdatedAt], [Deleted]) VALUES (N'3', N'Ahsoka''s lightsabers', N'As a Padawan, Ahsoka Tano used a green-bladed lightsaber, often wielding it in a non-standard reverse grip. Under the tutelage of Anakin Skywalker, Ahsoka became a skilled duelist, and built a second green-bladed lightsaber with a shorter, shoto-style blade. She used this shorter-bladed lightsaber to complement her primary weapon. During her time with the rebellion, Ahsoka used a pair of new lightsabers with white plasma blades.', NULL, N'2/28/2017 2:37:44 PM +00:00', N'2/28/2017 2:37:44 PM +00:00', 0)

INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'1', N'1')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'2', N'1')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'3', N'1')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'3', N'2')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'4', N'2')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'5', N'2')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'1', N'3')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'2', N'3')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'3', N'3')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'6', N'3')
INSERT INTO [dbo].[MovieCharacters] ([Movie_Id], [Character_Id]) VALUES (N'2', N'4')

INSERT INTO [dbo].[WeaponCharacters] ([Weapon_Id], [Character_Id]) VALUES (N'1', N'1')
INSERT INTO [dbo].[WeaponCharacters] ([Weapon_Id], [Character_Id]) VALUES (N'2', N'4')
INSERT INTO [dbo].[WeaponCharacters] ([Weapon_Id], [Character_Id]) VALUES (N'3', N'4')
