# CodeStar-Summer1403-SE-Team02

This repository is part of the CodeStar Bootcamp 1403 organized by Mohimian. The exercises focused on the following learning outcomes:

- C#
- Clean Code
- Test-Driven Development (TDD)
- SQL
- EFCore (Entity Framework Core)
- ASP.Net Core
- Unit Testing
- Pair Programming

---

## Phase 05

In this phase, a project is developed using C# and the .NET framework to create an **Inverted Index** from a set of documents. The program reads the documents and allows the user to input a word via the console, returning the names of the documents that contain that word.

### Features
- The project accepts three types of user inputs:
  1. **Mandatory Keywords**: Words that must be present in the results (no prefix).
  2. **Optional Keywords**: At least one of these words must be present, prefixed with `+`.
  3. **Excluded Keywords**: These words should not be present, prefixed with `-`.

  **Input Example**: 
  `get +illness +disease -cough -"star academy"`
  - `get`: must be included in the results
  - `+illness +disease`: at least one of these words must be present
  - `-cough`: must not be present
  - `-"star academy"`: this phrase must not be included

### Additional Functionality
- The program supports searching for phrases as well.

---

## Phase 06

In Phase 06, the project transitions to the **ASP.Net** framework and introduces the following enhancements:
- Implementation of **API endpoints** for search and document upload.
- The **Inverted Index** is now stored in a database using **EFCore**.

---

## Conclusion

This project exemplifies the application of advanced programming concepts and tools, providing a solid foundation in full-stack development with a focus on search functionality and database management.

---

## Collaborators
This project was developed by:
- [Mohammad Saleh Mahdinjead](https://github.com/msmahdinejad)
- [Kimia Kabiri](https://github.com/K-Kabiri)
