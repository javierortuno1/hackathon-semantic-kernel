# hackathon-semantic-kernel

### Semantic Kernel
- Open-Source SDK that allows to interact with LLM and AI tools
- Semantic functions -> Model API calls with the input and prompt
- Agents -> software tool that has a specific behaviour based on prompots, it will perform a smart loop by calling semantic functions agaings the model
    - it interacts with the same LLM but in a smart wy repeating the prompts
    - Plugins/Skills it is able to connect to other API services, tools, DBs, models, etc
    - Even to vector databases
        - Encoders for inputs. Allowing to encode the input data into a vector
        - those vectors will allow Similaritz search since similar vectors will be on the same space or at leas close

Therefore we can build a REST API that will use this sdk as a framework for setting up a config or interface interacting with a LLM, Any other Model, Agents and even connecting Vector databases for similiarities

The key is that Semantic Kernel is model-agnostic - you can use:

OpenAI models
HuggingFace models
Local models
Custom model deployments

https://www.youtube.com/watch?v=4_S3EYWfBZ8&ab_channel=AliIssa
