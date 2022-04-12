using Services.Contracts;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests
{
    public class GameServiceYearOnlyTests
    {
        [Fact]
        public void Check_OneEventPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract {date = "1"};

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "1" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "0"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "1"},
                new EventContract {date = "2"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "1"},
                new EventContract {date = "2"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "3"},
                new EventContract {date = "3"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "3"},
                new EventContract {date = "3"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "3"},
                new EventContract {date = "3"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "3"},
                new EventContract {date = "5"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace = new EventContract { date = "5" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "3"},
                new EventContract {date = "5"},
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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
        public void Check_IdenticalEventsPlacement_Incorrect()
        {
            // Arrange
            var gameService = new GameService(null, null);

            var eventToPlace = new EventContract { date = "3" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "3"},
                new EventContract {date = "3"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace,
                usedEvents = eventsPlaced,
                fullDates = false
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

            var eventToPlace1 = new EventContract { date = "3" };
            var eventToPlace2 = new EventContract { date = "5" };

            var eventsPlaced = new List<EventContract>
            {
                new EventContract {date = "-2"},
                new EventContract {date = "3"},
                new EventContract {date = "3"},
                new EventContract {date = "4"},
                new EventContract {date = "10"}
            };

            var game = new GameInstanceContract
            {
                mistakenEvents = new List<EventContract>(),
                lastEventContractSent = eventToPlace1,
                usedEvents = eventsPlaced,
                fullDates = false
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