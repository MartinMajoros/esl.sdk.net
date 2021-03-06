﻿using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    /// <summary>
    /// Example of how to configure the Question&Answer authentication method for a signer. The answer is given for testing 
    /// purposes. Never include the answer when creating packages for actual customers.
    /// </summary>
    public class SignerQnAChallengeExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new SignerQnAChallengeExample(Props.GetInstance()).Run();
        }

        public string email1;
        private Stream fileStream1;

        public readonly string FIRST_QUESTION = "What's your favorite sport? (answer: golf)";
        public readonly string FIRST_ANSWER = "golf";
        public readonly string SECOND_QUESTION = "What music instrument do you play? (answer: drums)";
        public readonly string SECOND_ANSWER = "drums";

        public SignerQnAChallengeExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public SignerQnAChallengeExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("SignerQnAChallengeExample: " + DateTime.Now)
                .DescribedAs("This is a Q&A authentication example")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .ChallengedWithQuestions(ChallengeBuilder.FirstQuestion(FIRST_QUESTION)
                        .Answer(FIRST_ANSWER)
                        .SecondQuestion(SECOND_QUESTION)
                        .AnswerWithMaskInput(SECOND_ANSWER)))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(199, 100)))
                .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);

            eslClient.SendPackage(packageId);
        }
    }
}

