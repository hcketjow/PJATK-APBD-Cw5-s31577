using System;

namespace PreparationForExam.Exceptions;

public class NotFoundException(string message) : Exception(message);
