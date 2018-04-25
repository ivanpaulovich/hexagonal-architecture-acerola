At first, let me explain the characteristics of an application with this architecture style.

* The business rules known as the Domain + the Application does not know the adapters implementations.
* The business rules and the adapters can be mocked and tested independently.
* The final software has pieces that can be changed easily.
* The software is independent of frameworks.
* The software is independent of databases.
* The software can evolve faster.

And to visually explain it check out the diagram below:

1. At the center we have the Domain with a potato shape and there is reason for it. Every business domain has its own rules, different specifications from each other, that is the reason of this undefined shape. For instance in our very specific example we design it with DDD Patterns.
2. The application has an hexagonal shape because each of its sides has specifics protocols, in our example we have Commands and Queries giving access to the Application Services.
3. The ports and adapters are implemented outside of the application as plugins.
4. Externally we have other systems.
