# 05 — Admin MVP (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 04 concluída e API funcional para operações básicas.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Criar a área administrativa mínima para cadastrar e manter conteúdo real do sistema, já conectada à api.

## Escopo obrigatório
- Criar a casca da área Admin separada da experiência do aluno.
- Implementar telas/fluxos de cadastro e edição de matéria.
- Implementar telas/fluxos de cadastro e edição de questão e alternativas.
- Implementar telas/fluxos de cadastro e edição de videoaula.
- Implementar telas/fluxos de cadastro e edição de material de estudo.
- Consumir a API real; sem acoplamento direto a Domain/Application/Infrastructure.

## Fora de escopo / proibido nesta etapa
- Não pular direto para dashboards complexos, relatórios ou recursos de gestão avançados.
- Não criar integrações diretas com banco.
- Não misturar a navegação do aluno com a do Admin.
- Não usar dados mockados se a API já existe.

## Regra de bloqueio desta etapa
Sem Admin funcional, o ecossistema fica sem conteúdo real. O agente deve parar se a API não sustentar o CRUD mínimo ou se a separação Admin/aluno ficar confusa.

## Regras técnicas específicas
- Priorizar fluxo funcional acima de polimento visual extremo.
- Formulários precisam validar entradas mínimas coerentes.
- Não criar uma arquitetura paralela dentro da Web só para o Admin.

## Entregas esperadas
- Estrutura da área Admin.
- Páginas/telas de cadastro e edição.
- Clientes HTTP/serviços de integração com API.
- Validação mínima de formulários e estados de erro.

## Critérios de pronto
- O admin consegue cadastrar e editar conteúdo essencial do MVP.
- As telas estão integradas à API.
- O conteúdo criado fica pronto para ser consumido na experiência do aluno.
- A área administrativa está funcional, mesmo que ainda simples visualmente.

## Validação obrigatória antes de aceitar a etapa
- Verificar se o Admin consome a API e não o banco.
- Verificar se os principais conteúdos do sistema podem ser mantidos por essa área.
- Verificar se a etapa prepara o terreno da Web do aluno.

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
06 — Web do aluno: estrutura inicial

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
05 — Admin MVP

Dependência obrigatória para iniciar:
Etapa 04 concluída e API funcional para operações básicas.

Regra de bloqueio:
Sem Admin funcional, o ecossistema fica sem conteúdo real. O agente deve parar se a API não sustentar o CRUD mínimo ou se a separação Admin/aluno ficar confusa.

Objetivo da etapa:
criar a área administrativa mínima para cadastrar e manter conteúdo real do sistema, já conectada à API

Escopo obrigatório:
- Criar a casca da área Admin separada da experiência do aluno.
- Implementar telas/fluxos de cadastro e edição de matéria.
- Implementar telas/fluxos de cadastro e edição de questão e alternativas.
- Implementar telas/fluxos de cadastro e edição de videoaula.
- Implementar telas/fluxos de cadastro e edição de material de estudo.
- Consumir a API real; sem acoplamento direto a Domain/Application/Infrastructure.

Não faça nesta etapa:
- Não pular direto para dashboards complexos, relatórios ou recursos de gestão avançados.
- Não criar integrações diretas com banco.
- Não misturar a navegação do aluno com a do Admin.
- Não usar dados mockados se a API já existe.

Regras técnicas específicas desta etapa:
- Priorizar fluxo funcional acima de polimento visual extremo.
- Formulários precisam validar entradas mínimas coerentes.
- Não criar uma arquitetura paralela dentro da Web só para o Admin.

Entregas esperadas:
- Estrutura da área Admin.
- Páginas/telas de cadastro e edição.
- Clientes HTTP/serviços de integração com API.
- Validação mínima de formulários e estados de erro.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O admin consegue cadastrar e editar conteúdo essencial do MVP.
- As telas estão integradas à API.
- O conteúdo criado fica pronto para ser consumido na experiência do aluno.
- A área administrativa está funcional, mesmo que ainda simples visualmente.

Checklist de validação antes de encerrar:
- Verificar se o Admin consome a API e não o banco.
- Verificar se os principais conteúdos do sistema podem ser mantidos por essa área.
- Verificar se a etapa prepara o terreno da Web do aluno.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
