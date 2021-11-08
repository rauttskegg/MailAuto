﻿//
// SaslMechanismAnonymousTests.cs
//
// Author: Jeffrey Stedfast <jestedfa@microsoft.com>
//
// Copyright (c) 2013-2021 .NET Foundation and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Net;
using System.Text;

using NUnit.Framework;

using MailKit.Security;

namespace UnitTests.Security {
	[TestFixture]
	public class SaslMechanismAnonymousTests
	{
		[Test]
		public void TestArgumentExceptions ()
		{
			var credentials = new NetworkCredential ("username", "password");
			SaslMechanism sasl;

			sasl = new SaslMechanismAnonymous (credentials);
			Assert.DoesNotThrow (() => sasl.Challenge (null));

			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous (null, credentials));
			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous (Encoding.UTF8, (NetworkCredential) null));
			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous ((NetworkCredential) null));
			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous (null, "username"));
			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous (Encoding.UTF8, (string) null));
			Assert.Throws<ArgumentNullException> (() => new SaslMechanismAnonymous ((string) null));
		}

		static void AssertAnonymous (SaslMechanismAnonymous sasl, string prefix)
		{
			const string expected = "dXNlcm5hbWU=";
			string challenge;

			Assert.IsFalse (sasl.SupportsChannelBinding, "{0}: SupportsChannelBinding", prefix);
			Assert.IsTrue (sasl.SupportsInitialResponse, "{0}: SupportsInitialResponse", prefix);

			challenge = sasl.Challenge (string.Empty);

			Assert.AreEqual (expected, challenge, "{0}: challenge response does not match the expected string.", prefix);
			Assert.IsTrue (sasl.IsAuthenticated, "{0}: should be authenticated.", prefix);
			Assert.IsFalse (sasl.NegotiatedChannelBinding, "{0}: NegotiatedChannelBinding", prefix);
			Assert.IsFalse (sasl.NegotiatedSecurityLayer, "{0}: NegotiatedSecurityLayer", prefix);

			Assert.AreEqual (string.Empty, sasl.Challenge (string.Empty), "{0}: challenge while authenticated.", prefix);
		}

		[Test]
		public void TestAnonymousAuth ()
		{
			var credentials = new NetworkCredential ("username", "password");
			var sasl = new SaslMechanismAnonymous (credentials);

			AssertAnonymous (sasl, "NetworkCredential");

			sasl = new SaslMechanismAnonymous ("username");

			AssertAnonymous (sasl, "user");
		}
	}
}