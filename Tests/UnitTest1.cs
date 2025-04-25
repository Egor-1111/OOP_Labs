using NUnit.Framework;
using OOP_Lab2;
using OOP_Lab2.ChangeFormatAdapters;
using OOP_Lab2.Command;
using OOP_Lab2.FileSafe;
using OOP_Lab2.UserStrategy;
using OOP_Lab2.StyleDecorator;

namespace OOP_Lab2Tests
{
    public class Tests
    {

        [Test]
        public void CurrentToJSONAdapter_Parse_ChangesDocumentTypeAndName()
        {

            var adapter = new CurrentToJSONAdapter();
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var document = new Document(admin, "1.txt", 0);
            IUserStrategy str = new ReaderStrategy();

            adapter.Parse(str, cloud, document);


            Assert.AreEqual(1, document.type);
            Assert.AreEqual("1.json", document.name);
        }



        [Test]
        public void CurrentToMarkdownAdapter_FromXMLToMarkdown_ConvertsCorrectly()
        {

            var adapter = new CurrentToMarkdownAdapter();
            string xml = "<b><i>bold italic</i></b> text";


            string result = adapter.FromXMLToMarkdown(xml);


            Assert.AreEqual("***bold italic*** text", result);
        }


        [Test]
        public void CurrentToMarkdownAdapter_FromRtfToMarkdown_ConvertsCorrectly()
        {

            string rtf = @"\b\i bold italic\i0\b0 text";


            string result = CurrentToMarkdownAdapter.FromRtfToMarkdown(rtf);


            Assert.AreEqual("*** bold italic*** text", result);
        }

        [Test]
        public void CurrentToMarkdownAdapter_Parse_XMLToMarkdownConversion()
        {

            var adapter = new CurrentToMarkdownAdapter();
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var document = new Document(admin, "test.xml", 3);
            IUserStrategy str = new ReaderStrategy();
            document.SetText("<b>bold</b> text");

            adapter.Parse(str, cloud, document);


            Assert.AreEqual(2, document.type);
            Assert.AreEqual("test.md", document.name);
            Assert.AreEqual("**bold** text", document.GetText());
        }

        [Test]
        public void CurrentToRTFAdapter_FromMarkdownToRtf_ConvertsCorrectly()
        {

            string markdown = "***bold italic*** text";


            string result = CurrentToRTFAdapter.FromMarkdownToRtf(markdown);


            Assert.AreEqual(@"\b\ibold italic\i0\b0 text", result);
        }

        [Test]
        public void CurrentToRTFAdapter_FromXmlToRtf_ConvertsCorrectly()
        {

            string xml = "<b><i>bold italic</i></b> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\b\ibold italic\i0\b0 text", result);
        }

        [Test]
        public void CurrentToRTFAdapter_Parse_MarkdownToRtfConversion()
        {
            var adapter = new CurrentToMarkdownAdapter();
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var document = new Document(admin, "test.md", 2);
            IUserStrategy str = new ReaderStrategy();
            document.SetText("**bold** text");

            adapter.Parse(str, cloud, document);


            Assert.AreEqual(2, document.type);
            Assert.AreEqual("test.md", document.name);
            Assert.AreEqual("**bold** text", document.GetText());
        }

        [Test]
        public void FromMarkdownToRtf_ConvertsItalicText()
        {

            string markdown = "*italic* text";


            string result = CurrentToRTFAdapter.FromMarkdownToRtf(markdown);


            Assert.AreEqual(@"\iitalic\i0 text", result);
        }

        [Test]
        public void FromMarkdownToRtf_ConvertsBoldItalicText()
        {

            string markdown = "***bold italic*** text";


            string result = CurrentToRTFAdapter.FromMarkdownToRtf(markdown);


            Assert.AreEqual(@"\b\ibold italic\i0\b0 text", result);
        }

        [Test]
        public void FromMarkdownToRtf_ConvertsUnderlinedText()
        {

            string markdown = "<u>underlined</u> text";


            string result = CurrentToRTFAdapter.FromMarkdownToRtf(markdown);


            Assert.AreEqual(@"\uunderlined\ul0 text", result);
        }

        [Test]
        public void FromMarkdownToRtf_ConvertsCombinedFormats()
        {

            string markdown = "***<u> bold italic underline</u>***text";


            string result = CurrentToRTFAdapter.FromMarkdownToRtf(markdown);


            Assert.AreEqual(@"\b\i\u bold italic underline\ul0\i0\b0text", result);
        }

        [Test]
        public void FromXmlToRtf_ConvertsBoldText()
        {

            string xml = "<b>bold</b> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\bbold\b0 text", result);
        }

        [Test]
        public void FromXmlToRtf_ConvertsItalicText()
        {

            string xml = "<i>italic</i> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\iitalic\i0 text", result);
        }

        [Test]
        public void FromXmlToRtf_ConvertsBoldItalicText()
        {

            string xml = "<b><i>bold italic</i></b> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\b\ibold italic\i0\b0 text", result);
        }

        [Test]
        public void FromXmlToRtf_ConvertsUnderlinedText()
        {

            string xml = "<u>underlined</u> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\ulunderlined\ul0 text", result);
        }

        [Test]
        public void FromXmlToRtf_ConvertsCombinedFormats()
        {

            string xml = "<b><i><u>bold italic underline</u></i></b> text";


            string result = CurrentToRTFAdapter.FromXmlToRtf(xml);


            Assert.AreEqual(@"\b\i\ulbold italic underline\ul0\i0\b0 text", result);
        }



        [Test]
        public void FromMarkdownToXml_ConvertsBoldText()
        {

            string markdown = "**bold** text";


            string result = CurrentToXMLAdapter.FromMarkdownToXml(markdown);


            Assert.AreEqual("<b>bold</b> text", result);
        }

        [Test]
        public void FromMarkdownToXml_ConvertsItalicText()
        {

            string markdown = "*italic* text";


            string result = CurrentToXMLAdapter.FromMarkdownToXml(markdown);


            Assert.AreEqual("<i>italic</i> text", result);
        }

        [Test]
        public void FromMarkdownToXml_ConvertsBoldItalicText()
        {

            string markdown = "***bold italic*** text";


            string result = CurrentToXMLAdapter.FromMarkdownToXml(markdown);


            Assert.AreEqual("<b><i>bold italic</i></b> text", result);
        }

        [Test]
        public void FromMarkdownToXml_ConvertsUnderlinedText()
        {

            string markdown = "~~underlined~~ text";


            string result = CurrentToXMLAdapter.FromMarkdownToXml(markdown);


            Assert.AreEqual("<u>underlined</u> text", result);
        }

        [Test]
        public void FromMarkdownToXml_ConvertsCombinedFormats()
        {

            string markdown = "**_bold italic_** text";


            string result = CurrentToXMLAdapter.FromMarkdownToXml(markdown);


            Assert.AreEqual("<b><i>bold italic</i></b> text", result);
        }

        [Test]
        public void FromRtfToXml_ConvertsBoldText()
        {

            string rtf = @"\bbold\b0 text";


            string result = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<b>bold</b> text", result);
        }

        [Test]
        public void FromRtfToXml_ConvertsItalicText()
        {

            string rtf = @"\iitalic\i0 text";


            string result = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<i>italic</i> text", result);
        }

        [Test]
        public void FromRtfToXml_ConvertsBoldItalicText()
        {

            string rtf = @"\b\ibold italic\i0\b0 text";


            string result = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<b><i>bold italic</i></b> text", result);
        }

        [Test]
        public void FromRtfToXml_ConvertsUnderlinedText()
        {

            string rtf = @"\ulunderlined\ul0 text";


            string result = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<u>underlined</u> text", result);
        }

        [Test]
        public void FromRtfToXml_ConvertsCombinedFormats()
        {

            string rtf = @"\b\i\ulbold italic underline\ul0\i0\b0 text";


            string result = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<b><i><u>bold italic underline</u></i></b> text", result);
        }



        [Test]
        public void MarkdownToXmlAndBack_PreservesFormatting()
        {

            string original = "***bold italic*** and **bold** and *italic* and ~~underline~~";


            string xml = CurrentToXMLAdapter.FromMarkdownToXml(original);
            string markdown = new CurrentToMarkdownAdapter().FromXMLToMarkdown(xml);


            Assert.AreEqual("***bold italic*** and **bold** and *italic* and <u>underline</u>", markdown);
        }

        [Test]
        public void XmlToRtfAndBack_PreservesFormatting()
        {

            string original = "<b><i>bold italic</i></b> and <b>bold</b> and <i>italic</i> and <u>underline</u>";


            string rtf = CurrentToRTFAdapter.FromXmlToRtf(original);
            string xml = CurrentToXMLAdapter.FromRtfToXml(rtf);


            Assert.AreEqual("<b><i>bold italic</i></b> and <b>bold</b> and <i>italic</i> and <u>underline</u>", xml);
        }
        [Test]
        public void CommandManager_Save_AddsFrameToUndoStack()
        {
            // Arrange
            var manager = new CommandManager();
            var frame = new Frame("test", 0, 0, "");

            // Act
            manager.Save(frame);

            // Assert
            Assert.AreEqual(1, manager.GetUndoStackCount());
            Assert.AreEqual(0, manager.GetRedoStackCount());
        }

        [Test]
        public void CommandManager_Save_WhenOverLimit_RemovesOldestFrame()
        {
            var manager = new CommandManager();

            for (int i = 0; i < 251; i++)
            {
                manager.Save(new Frame($"frame{i}", 0, 0, ""));
            }
            Assert.AreEqual(250, manager.GetUndoStackCount());
            Assert.AreEqual("frame1", manager.GetUndoStackFirstFrame().txt);
            Assert.AreEqual("frame250", manager.GetUndoStackLastFrame().txt);
        }




        [Test]
        public void CommandManager_Undo_WhenOnlyOneState_ReturnsEmptyString()
        {
            var manager = new CommandManager();
            var frame = new Frame("test", 0, 0, "");
            manager.Save(frame);

            var result = manager.UnDo();

            Assert.AreEqual("", result);
            Assert.AreEqual(1, manager.GetUndoStackCount());
            Assert.AreEqual(0, manager.GetRedoStackCount());
        }



        [Test]
        public void CommandManager_Redo_WhenNoRedoStates_ReturnsEmptyString()
        {
            var manager = new CommandManager();
            var frame = new Frame("test", 0, 0, "");
            manager.Save(frame);

            var result = manager.ReDo();


            Assert.AreEqual("", result);
            Assert.AreEqual(1, manager.GetUndoStackCount());
            Assert.AreEqual(0, manager.GetRedoStackCount());
        }
        [Test]
        public void Create_WhenFileNotExists_ShouldCreateFile()
        {
            WorkWithLocal _fileWorker = new WorkWithLocal();
            _fileWorker.Create("1.txt", "test content");

            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
            Assert.AreEqual("test content", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
        }

        [Test]
        public void Delete_WhenFileExists_ShouldRemoveFile()
        {
            WorkWithLocal _fileWorker = new WorkWithLocal();
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "1.txt", "content");

            _fileWorker.Delete("1.txt");

            Assert.IsFalse(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
        }

        [Test]
        public void Delete_WhenFileNotExists_ShouldNotThrow()
        {
            WorkWithLocal _fileWorker = new WorkWithLocal();
            Assert.DoesNotThrow(() => _fileWorker.Delete("nonexistent_file.txt"));
        }

        [Test]
        public void Update_WhenFileExists_ShouldChangeContent()
        {
            WorkWithLocal _fileWorker = new WorkWithLocal();
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "1.txt", "old content");

            _fileWorker.Update("1.txt", "new content");

            Assert.AreEqual("new content", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
        }

        [Test]
        public void Update_WhenFileNotExists_ShouldCreateFile()
        {
            WorkWithLocal _fileWorker = new WorkWithLocal();
            _fileWorker.Update("1.txt", "content");

            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
            Assert.AreEqual("content", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "1.txt"));
        }
        [Test]
        public void MarkdownDecorator_Compile_WithBoldText_AddsBoldEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("**bold** text");
            var decorator = new MarkdownDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[1mbold\u001b[0m text", result);
        }

        [Test]
        public void MarkdownDecorator_Compile_WithItalicText_AddsItalicEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("*italic* text");
            var decorator = new MarkdownDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[3mitalic\u001b[0m text", result);
        }

        [Test]
        public void MarkdownDecorator_Compile_WithUnderlinedText_AddsUnderlineEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("<u>underlined</u> text");
            var decorator = new MarkdownDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[4munderlined\u001b[0m text", result);
        }

        [Test]
        public void MarkdownDecorator_Compile_WithBoldItalicText_AddsCombinedEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("***bold italic*** text");
            var decorator = new MarkdownDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[1;3mbold italic\u001b[0m text", result);
        }

        [Test]
        public void RtfDecorator_Compile_WithBoldText_AddsBoldEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText(@"\bbold\b0 text");
            var decorator = new RtfDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[1mbold\u001b[0m text", result);
        }

        [Test]
        public void RtfDecorator_Compile_WithItalicText_AddsItalicEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText(@"\iitalic\i0 text");
            var decorator = new RtfDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[3mitalic\u001b[0m text", result);
        }


        [Test]
        public void XmlDecorator_Compile_WithBoldText_AddsBoldEscapeCodes()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("<b>bold</b> text");
            var decorator = new XmlDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("\u001b[1mbold\u001b[0m text", result);
        }



        [Test]
        public void JsonDecorator_Compile_ReturnsOriginalText()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("{\"text\":\"value\"}");
            var decorator = new JsonDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("{\"text\":\"value\"}", result);
        }

        [Test]
        public void TxtDecorator_Compile_ReturnsOriginalText()
        {
            List<Document> docs = new List<Document>();
            var admin = new User("admin", "1", docs);
            var cloud = new WorkWithCloud();
            var doc = new Document(admin, "1.txt", 0);
            doc.SetText("plain text");
            var decorator = new TxtDecorator(doc);

            var result = decorator.Compile();

            Assert.AreEqual("plain text", result);
        }
        [Test]
        public void CheckWithBorders_ValidInput_ReturnsNumber()
        {
            // Arrange
            var checker = new Checker();
            string input = "5";
            int low = 1;
            int big = 10;
            string possibleErr = "??????? ????? ?? 1 ?? 10";
            int borders = 1;

            // Act
            int result = checker.CheckWithBorders(input, low, big, possibleErr, borders);

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void CheckStringInCollection_UserExists_ReturnsTrue()
        {
            // Arrange
            var checker = new Checker();
            var users = new List<User>
            {
                new User("admin", "user1", new List<Document>()),
                new User("reader", "user2", new List<Document>())
            };
            string searchName = "user1";

            // Act
            bool result = checker.CheckStringInCollection(users, searchName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckStringInCollection_UserNotExists_ReturnsFalse()
        {
            // Arrange
            var checker = new Checker();
            var users = new List<User>
            {
                new User("admin", "user1", new List<Document>()),
                new User("reader", "user2", new List<Document>())
            };
            string searchName = "user3";

            // Act
            bool result = checker.CheckStringInCollection(users, searchName);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckStringInCollectionDoc_DocumentExists_ReturnsTrue()
        {
            // Arrange
            var checker = new Checker();
            var docs = new List<Document>
            {
                new Document(new User("admin", "user1", new List<Document>()),"doc1.txt",0),
                new Document(new User("reader", "user2", new List<Document>()),"doc1.txt",0)
            };
            string searchName = "doc1.txt";

            // Act
            bool result = checker.CheckStringInCollectionDoc(docs, searchName);

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void Notify_AddsInfoToAllSubscribedUsers()
        {
            // Arrange
            var eventManager = new EventManager();
            var user1 = new User("admin", "user1", new List<Document>());
            var user2 = new User("reader", "user2", new List<Document>());

            eventManager.Subscribe(user1);
            eventManager.Subscribe(user2);
            string notification = "Test notification";

            // Act
            eventManager.Notify(notification);

            // Assert
            Assert.AreEqual(notification, user1.Logs);
            Assert.AreEqual(notification, user2.Logs);
        }

        [Test]
        public void Subscribe_AddsUserToListeners()
        {
            // Arrange
            var eventManager = new EventManager();
            var user = new User("admin", "user1", new List<Document>());

            // Act
            eventManager.Subscribe(user);

            // Assert
            Assert.Contains(user, eventManager.GetListeners());
        }

        [Test]
        public void SetStrategy_Admin_SetsAdminStrategy()
        {
            // Arrange
            var user = new User("admin", "testuser", new List<Document>());

            // Act
            user.SetStrategy();

            // Assert
            Assert.IsInstanceOf<AdminStrategy>(user.GetStrategy());
        }

        [Test]
        public void SetDocument_AssignsTemporaryDocument()
        {
            // Arrange
            var user = new User("admin", "testuser", new List<Document>());
            var doc = new Document(user, "test.txt", 0);

            // Act
            user.SetDocument(doc);

            // Assert
            Assert.AreEqual("test.txt", user.GetTemporaryDocument().name);
        }

        [Test]
        public void CheckLogs_ReturnsNumberOfLogEntries()
        {
            // Arrange
            var user = new User("admin", "testuser", new List<Document>());
            user.Logs = "log1\nlog2\nlog3";

            // Act
            int count = user.CheckLogs();

            // Assert
            Assert.AreEqual(3, count);
        }
    }

}
internal static class TestHelpers
{
    public static IUserStrategy GetStrategy(this User user)
    {
        return (IUserStrategy)typeof(User)
            .GetProperty("Strategy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(user);
    }

    public static Document GetTemporaryDocument(this User user)
    {
        return (Document)typeof(User)
            .GetProperty("TemporaryDocument", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(user);
    }

    public static List<User> GetListeners(this EventManager manager)
    {
        return (List<User>)typeof(EventManager)
            .GetField("listeners", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(manager);
    }
}



internal static class CommandManagerTestExtensions
{
    public static int GetUndoStackCount(this CommandManager manager)
    {
        return ((List<Frame>)typeof(CommandManager)
            .GetField("UnDoOp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(manager)).Count;
    }

    public static int GetRedoStackCount(this CommandManager manager)
    {
        return ((List<Frame>)typeof(CommandManager)
            .GetField("ReDoOp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(manager)).Count;
    }

    public static Frame GetUndoStackFirstFrame(this CommandManager manager)
    {
        var stack = (List<Frame>)typeof(CommandManager)
            .GetField("UnDoOp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(manager);
        return stack[0];
    }

    public static Frame GetUndoStackLastFrame(this CommandManager manager)
    {
        var stack = (List<Frame>)typeof(CommandManager)
            .GetField("UnDoOp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(manager);
        return stack[stack.Count - 1];
    }
}
