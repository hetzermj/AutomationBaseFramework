using FluentAssertions;
using NUnit.Framework;

namespace AutomationBase.Tests
{
	public class FluentAssertionsExamples
	{

		[Test]
		public void NUnitTestFail1()
		{
			Person p1 = new Person();
			Person p2 = new Person();

			p1.First = "Mike";
			p1.Last = "Hetzer";

			p2.First = "Mike";
			p2.Last = "Hetzer";

			Assert.That(p1, Is.EqualTo(p2)); // Assertion fails because EqualTo checks by reference - objects do not share same memory location
		}

		[Test]
		public void FluentTestPass1()
		{
			Person p1 = new Person();
			Person p2 = new Person();

			p1.First = "Mike";
			p1.Last = "Hetzer";

			p2.First = "Mike";
			p2.Last = "Hetzer";

			p1.Should().BeEquivalentTo(p2); // Passes because both object share equivalent properties
		}

		[Test]
		public void FluentTestFail1()
		{
			Person p1 = new Person();
			Person p2 = new Person();

			p1.First = "Mike";
			p1.Last = "Hetzer";

			p2.First = "Mike";
			p2.Last = "hetzer";

			p1.Should().BeEquivalentTo(p2); // Fails because of lowercase Last property
		}

		[Test]
		public void FluentTestFail2()
		{
			Person p1 = new Person();
			Person p2 = new Person();

			p1.First = "Mike";
			p1.Last = "Hetzer";

			p2.First = "Mike";
			p2.Last = "Hetzer";
			p2.Age = 32;

			p1.Should().BeEquivalentTo(p2); // Fails because p2 has another property assigned a value whereas same property in p1 is null or default
		}


	}

	public class Person
	{
		public string First { get; set; }
		public string Last { get; set; }
		public int? Age { get; set; }
	}
}
