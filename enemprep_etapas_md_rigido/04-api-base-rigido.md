# 04 — API Base (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapas 02 e 03 concluídas; contratos e persistência precisam estar funcionais.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Expor a base da api do sistema com endpoints consistentes para os fluxos iniciais do mvp, mantendo controllers finos e regras na camada correta.

## Escopo obrigatório
- Criar controllers/endpoints para autenticação básica, usuário/perfil, matérias, questões, videoaulas e materiais conforme o necessário para o MVP inicial.
- Configurar DI, middlewares, tratamento de erro e padronização de rotas.
- Preparar documentação básica dos endpoints se fizer parte da convenção do projeto.
- Garantir serialização e contratos HTTP coerentes.

## Fora de escopo / proibido nesta etapa
- Não mover regra de negócio para controller.
- Não acessar banco diretamente do controller.
- Não criar telas web nesta etapa.
- Não expor entidades do Domain cruamente se houver DTO apropriado.

## Regra de bloqueio desta etapa
Se os endpoints estiverem inconsistentes ou sem contrato claro, Admin e Web serão bloqueados. O agente deve parar e relatar isso antes de avançar.

## Regras técnicas específicas
- Controller é borda de entrada, não local de regra de negócio.
- Rotas devem refletir o recurso e o caso de uso com clareza.
- Não criar endpoints por conveniência se eles não atendem o fluxo previsto no PRD.

## Entregas esperadas
- Controllers/endpoints.
- Program/bootstrapping da API.
- Configurações de DI, middleware e tratamento de exceções.
- Possível documentação inicial dos endpoints.

## Critérios de pronto
- A API sobe e atende os fluxos iniciais previstos.
- Os endpoints usam Application e Infrastructure de forma organizada.
- O tratamento de erro e as respostas básicas estão padronizados.
- A etapa deixa a Admin e a Web aptas a consumir dados reais.

## Validação obrigatória antes de aceitar a etapa
- Verificar se cada endpoint tem caso de uso claro.
- Verificar se a resposta HTTP não vaza detalhes internos desnecessários.
- Verificar se a API está pronta para ser consumida sem mocks.

## Formato obrigatório da resposta do agente
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

## Perguntas que o agente deve responder no fim
- O que foi implementado?
- O que não foi implementado?
- Existe algo que bloqueia a próxima etapa?
- Qual é a próxima etapa permitida do roadmap?
- Você confirma explicitamente que **não** adiantou trabalho da próxima etapa?

## Próxima etapa permitida
05 — Admin MVP

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
04 — API Base

Dependência obrigatória para iniciar:
Etapas 02 e 03 concluídas; contratos e persistência precisam estar funcionais.

Regra de bloqueio:
Se os endpoints estiverem inconsistentes ou sem contrato claro, Admin e Web serão bloqueados. O agente deve parar e relatar isso antes de avançar.

Objetivo da etapa:
expor a base da API do sistema com endpoints consistentes para os fluxos iniciais do MVP, mantendo controllers finos e regras na camada correta

Escopo obrigatório:
- Criar controllers/endpoints para autenticação básica, usuário/perfil, matérias, questões, videoaulas e materiais conforme o necessário para o MVP inicial.
- Configurar DI, middlewares, tratamento de erro e padronização de rotas.
- Preparar documentação básica dos endpoints se fizer parte da convenção do projeto.
- Garantir serialização e contratos HTTP coerentes.

Não faça nesta etapa:
- Não mover regra de negócio para controller.
- Não acessar banco diretamente do controller.
- Não criar telas web nesta etapa.
- Não expor entidades do Domain cruamente se houver DTO apropriado.

Regras técnicas específicas desta etapa:
- Controller é borda de entrada, não local de regra de negócio.
- Rotas devem refletir o recurso e o caso de uso com clareza.
- Não criar endpoints por conveniência se eles não atendem o fluxo previsto no PRD.

Entregas esperadas:
- Controllers/endpoints.
- Program/bootstrapping da API.
- Configurações de DI, middleware e tratamento de exceções.
- Possível documentação inicial dos endpoints.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A API sobe e atende os fluxos iniciais previstos.
- Os endpoints usam Application e Infrastructure de forma organizada.
- O tratamento de erro e as respostas básicas estão padronizados.
- A etapa deixa a Admin e a Web aptas a consumir dados reais.

Checklist de validação antes de encerrar:
- Verificar se cada endpoint tem caso de uso claro.
- Verificar se a resposta HTTP não vaza detalhes internos desnecessários.
- Verificar se a API está pronta para ser consumida sem mocks.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
