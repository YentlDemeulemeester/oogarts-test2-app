using Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Articles.DomainTests
{
    public class ArticleDomainTests
    {
        public void Article_WhenInitializedWithValidParameters_ShouldSetProperties()
        {
            //Arrange
            string title = "title";
            string description = "description";
            string content = "content";
            string imageurl = "imageurl";

            //Act
            Article article = new Article(title,description, content,imageurl);

            //Assert
            Assert.AreEqual(title,article.Title);
            Assert.AreEqual(description, article.Description);
            Assert.AreEqual(content, article.Content);
            Assert.AreEqual(imageurl, article.ImageUrl);
        }
    }
}
