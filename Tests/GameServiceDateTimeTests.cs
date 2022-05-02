using Services.Contracts;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests
{
    public class GameServiceDateTimeTests
    {
        [Fact]
        public void Check_OneEventPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-01"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 1;
            var expectedMistakenEvents = new List<EventContract> { eventToPlace };

            // Act
            gameService.IsPlacementCorrect(game, 0);

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_OneEventPlacement_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-01"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 1);

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_ManyEventsPlacement_Inorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "2021-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "1678-12-01"},
                new EventContract {date = "1789-09-07"},
                new EventContract {date = "1855-02-03"},
                new EventContract {date = "2021-02-04"},
                new EventContract {date = "2022-02-03"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 1;
            var expectedMistakenEvents = new List<EventContract> { eventToPlace };

            // Act
            gameService.IsPlacementCorrect(game, 2);

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_ManyEventsPlacement_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "2021-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "2-12-01"},
                new EventContract {date = "1789-09-07"},
                new EventContract {date = "1855-02-03"},
                new EventContract {date = "2021-02-04"},
                new EventContract {date = "2022-02-03"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 3);

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsPlacement1_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0020-05-01"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
                new EventContract {date = "2021-02-03"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 1); //Checking all 3 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsPlacement2_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0020-05-01"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
                new EventContract {date = "2021-02-03"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 2); //Checking all 3 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsPlacement3_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0020-05-01"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
                new EventContract {date = "2021-02-03"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 3); //Checking all 3 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsAtStartPlacement_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 0); //Checking 2 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsAtEndPlacement_Correct()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "2021-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 0;
            var expectedMistakenEvents = new List<EventContract>();

            // Act
            gameService.IsPlacementCorrect(game, 1); //Checking 2 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsAtStartPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 1;
            var expectedMistakenEvents = new List<EventContract> { eventToPlace };

            // Act
            gameService.IsPlacementCorrect(game, 2); //Checking 2 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsAtEndPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "2021-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 1;
            var expectedMistakenEvents = new List<EventContract> { eventToPlace };

            // Act
            gameService.IsPlacementCorrect(game, 0); //Checking 2 positions (relative order)

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_IdenticalEventsPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "123-02-03" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0020-05-01"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-03"},
                new EventContract {date = "2021-02-08"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes = 1;
            var expectedMistakenEvents = new List<EventContract> { eventToPlace };

            // Act
            gameService.IsPlacementCorrect(game, 0);

            var actualMistakes = game.mistakes;
            var actualMistakenEvents = game.mistakenEvents;

            // Assert
            Assert.Equal(expectedMistakes, actualMistakes);
            Assert.Equal(expectedMistakenEvents, actualMistakenEvents);
        }


        [Fact]
        public void Check_MultiplelEventPlacements_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace1 = new EventContract { date = "123-02-03" };
            var eventToPlace2 = new EventContract { date = "2022-03-08" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0020-05-01"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "123-02-03"},
                new EventContract {date = "2021-02-08"},
                new EventContract {date = "2022-02-08"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace1,
                usedEvents = eventsPlaced,
                fullDates = true
            };

            var expectedMistakes1 = 1;
            var expectedMistakenEvents1 = new List<EventContract> { eventToPlace1 };

            var expectedMistakes2 = 2;
            var expectedMistakenEvents2 = new List<EventContract> { eventToPlace1, eventToPlace2 };

            // Act 1
            gameService.IsPlacementCorrect(game, 0);
            var actualMistakes1 = game.mistakes;
            var actualMistakenEvents1 = game.mistakenEvents;

            // Assert 1
            Assert.Equal(expectedMistakes1, actualMistakes1);
            Assert.Equal(expectedMistakenEvents1, actualMistakenEvents1);


            // Act 2
            game.lastEventContractSent = eventToPlace2;
            gameService.IsPlacementCorrect(game, 0);
            var actualMistakes2 = game.mistakes;
            var actualMistakenEvents2 = game.mistakenEvents;

            // Assert 2
            Assert.Equal(expectedMistakes2, actualMistakes2);
            Assert.Equal(expectedMistakenEvents2, actualMistakenEvents2);
        }
    }
}
