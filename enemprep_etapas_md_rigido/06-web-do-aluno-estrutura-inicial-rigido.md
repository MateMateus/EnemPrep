# 06 — Web do Aluno: Estrutura Inicial (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapas 04 e 05 concluídas. A API deve estar utilizável e já deve existir conteúdo cadastrável pelo Admin.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Montar a experiência inicial do aluno com navegação, layout e consumo real da api, sem ainda fechar todos os fluxos profundos.

## Escopo obrigatório
- Criar layout global da Web do aluno com identidade jovem e responsiva.
- Implementar home/dashboard inicial simples.
- Implementar navegação para matérias, questões, videoaulas, materiais e perfil.
- Criar listagens básicas consumindo a API real.
- Implementar perfil básico do usuário na interface.

## Fora de escopo / proibido nesta etapa
- Não referenciar Domain/Application/Infrastructure diretamente.
- Não implementar lógica de negócio no front.
- Não fingir fluxo funcional com dados estáticos quando a API já oferece dados reais.
- Não tentar fechar todo o produto nesta etapa.

## Regra de bloqueio desta etapa
Se a Web não estiver consumindo a API corretamente, o fluxo de estudo fica artificial. O agente deve parar e corrigir a integração antes de seguir.

## Regras técnicas específicas
- A Web é cliente da API; essa relação não pode ser quebrada por conveniência.
- Priorizar estrutura, roteamento e integração antes de microdetalhes estéticos.
- Componentes devem preparar reuso sem criar abstrações prematuras.

## Entregas esperadas
- Layout e navegação.
- Páginas iniciais do aluno.
- Clientes/serviços HTTP.
- Estados básicos de carregamento, vazio e erro.

## Critérios de pronto
- A Web do aluno navega pelas áreas principais e consome a API.
- As páginas básicas estão responsivas e coerentes.
- Existe base real para entrar depois no fluxo profundo de resolução de questões.
- A separação Web/API está preservada.

## Validação obrigatória antes de aceitar a etapa
- Verificar que a Web não referencia as camadas proibidas.
- Verificar que há consumo real da API.
- Verificar que a UX inicial já suporta evolução natural para a próxima etapa.

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
07 — Fluxo real de estudo com questões

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
06 — Web do Aluno: Estrutura Inicial

Dependência obrigatória para iniciar:
Etapas 04 e 05 concluídas. A API deve estar utilizável e já deve existir conteúdo cadastrável pelo Admin.

Regra de bloqueio:
Se a Web não estiver consumindo a API corretamente, o fluxo de estudo fica artificial. O agente deve parar e corrigir a integração antes de seguir.

Objetivo da etapa:
montar a experiência inicial do aluno com navegação, layout e consumo real da API, sem ainda fechar todos os fluxos profundos

Escopo obrigatório:
- Criar layout global da Web do aluno com identidade jovem e responsiva.
- Implementar home/dashboard inicial simples.
- Implementar navegação para matérias, questões, videoaulas, materiais e perfil.
- Criar listagens básicas consumindo a API real.
- Implementar perfil básico do usuário na interface.

Não faça nesta etapa:
- Não referenciar Domain/Application/Infrastructure diretamente.
- Não implementar lógica de negócio no front.
- Não fingir fluxo funcional com dados estáticos quando a API já oferece dados reais.
- Não tentar fechar todo o produto nesta etapa.

Regras técnicas específicas desta etapa:
- A Web é cliente da API; essa relação não pode ser quebrada por conveniência.
- Priorizar estrutura, roteamento e integração antes de microdetalhes estéticos.
- Componentes devem preparar reuso sem criar abstrações prematuras.

Entregas esperadas:
- Layout e navegação.
- Páginas iniciais do aluno.
- Clientes/serviços HTTP.
- Estados básicos de carregamento, vazio e erro.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A Web do aluno navega pelas áreas principais e consome a API.
- As páginas básicas estão responsivas e coerentes.
- Existe base real para entrar depois no fluxo profundo de resolução de questões.
- A separação Web/API está preservada.

Checklist de validação antes de encerrar:
- Verificar que a Web não referencia as camadas proibidas.
- Verificar que há consumo real da API.
- Verificar que a UX inicial já suporta evolução natural para a próxima etapa.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
