insert into GameTitle (GameModeTitleCode, GameModeTitleName) values
("inf", "Infected");

-- Core Mode (GameTypeId = 1)
INSERT INTO GameMode (GameModeDescription, GameTypeId, GameTitleId) VALUES
(NULL, 1, 1),  -- TDM
(NULL, 1, 2),  -- S&D
(NULL, 1, 3),  -- FFA
(NULL, 1, 4),  -- HQ
(NULL, 1, 5),  -- Control
(NULL, 1, 6),  -- Overdrive
(NULL, 1, 7),  -- Domination
(NULL, 1, 8),  -- KC
(NULL, 1, 9),  -- Hardpoint
(NULL, 1, 10), -- Kill Order
(NULL, 1, 11); -- Ransack

-- Hardcore Mode (GameTypeId = 2)
INSERT INTO GameMode (GameModeDescription, GameTypeId, GameTitleId) VALUES
(NULL, 2, 1),  -- TDM
(NULL, 2, 2),  -- S&D
(NULL, 2, 3),  -- FFA
(NULL, 2, 4),  -- HQ
(NULL, 2, 5),  -- Control
(NULL, 2, 6),  -- Overdrive
(NULL, 2, 7),  -- Domination
(NULL, 2, 8),  -- KC
(NULL, 2, 9),  -- Hardpoint
(NULL, 2, 10), -- Kill Order
(NULL, 2, 11); -- Ransack

-- Faceoff Core Mode (GameTypeId = 4)
INSERT INTO GameMode (GameModeDescription, GameTypeId, GameTitleId) VALUES
(NULL, 4, 1),  -- TDM
(NULL, 4, 2),  -- S&D
(NULL, 4, 3),  -- FFA
(NULL, 4, 4),  -- HQ
(NULL, 4, 5),  -- Control
(NULL, 4, 6),  -- Overdrive
(NULL, 4, 7),  -- Domination
(NULL, 4, 8),  -- KC
(NULL, 4, 9),  -- Hardpoint
(NULL, 4, 10), -- Kill Order
(NULL, 4, 11); -- Ransack

-- Faceoff Hardcore Mode (GameTypeId = 5)
INSERT INTO GameMode (GameModeDescription, GameTypeId, GameTitleId) VALUES
(NULL, 5, 1),  -- TDM
(NULL, 5, 2),  -- S&D
(NULL, 5, 3),  -- FFA
(NULL, 5, 4),  -- HQ
(NULL, 5, 5),  -- Control
(NULL, 5, 6),  -- Overdrive
(NULL, 5, 7),  -- Domination
(NULL, 5, 8),  -- KC
(NULL, 5, 9),  -- Hardpoint
(NULL, 5, 10), -- Kill Order
(NULL, 5, 11); -- Ransack

-- Party Mode (GameTypeId = 3)
INSERT INTO GameMode (GameModeDescription, GameTypeId, GameTitleId) VALUES
(NULL, 3, 12), -- Third Wheel Gunfight
(NULL, 3, 13), -- Couples Dance Off
(NULL, 3, 14), -- Gun Game
(NULL, 3, 15); -- Prop Hunt
