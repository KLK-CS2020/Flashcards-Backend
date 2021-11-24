using Flashcards_backend.Core.Models;
using Xunit;

namespace Flashcards.Core.Test.Models
{
    public class DeckTest
    {
        private readonly Deck _deck;
        public DeckTest()
        {
            _deck = new Deck();
        }
        
        [Fact]
        public void Deck_CanBeInitialized()
        {
            Assert.NotNull(_deck);
        }
        
        [Fact]
        public void Deck_Id_MustBeInt()
        {
            Assert.True(_deck.Id is int);
        }

        [Fact]
        public void Deck_SetId_StoresId()
        {
            _deck.Id = 1;
            Assert.Equal(1, _deck.Id);
        }
        
        [Fact]
        public void Deck_UpdateId_StoresNewId()
        {
            _deck.Id = 1;
            _deck.Id = 2;
            Assert.Equal(2, _deck.Id);
        }

        [Fact]
        public void Deck_SetName_StoreNameAsString()
        {
            _deck.Name = "animals";
            Assert.Equal("animals",  _deck.Name);
            
        }
        
        [Fact]
        public void Deck_SetDescription_StoreDescriptionAsString()
        {
            _deck.Description = "learn animal names";
            Assert.Equal("learn animal names",  _deck.Description);
            
        }
        
        [Fact]
        public void Deck_SetIsPublic_StoreAsBool()
        {
            _deck.isPublic = true;
            Assert.True(_deck.isPublic);
        }
        
        [Fact]
        public void Deck_UpdateIsPublic_StoreNewIsPublic()
        {
            _deck.isPublic = true;
            _deck.isPublic = false;
            Assert.False(_deck.isPublic);
        }
    }
}