# 07 — Fluxo Real de Estudo com Questões (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 06 concluída e integração Web/API funcionando.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Entregar o primeiro ciclo completo de valor para o aluno: escolher, responder e registrar questões com feedback real.

## Escopo obrigatório
- Implementar tela de resolução de questão.
- Permitir envio de resposta.
- Exibir feedback de acerto/erro.
- Registrar tentativa do usuário via API.
- Criar histórico simples ou visão mínima das tentativas.
- Permitir indicadores básicos por matéria ou assunto, se já estiverem previstos nos contratos.

## Fora de escopo / proibido nesta etapa
- Não transformar essa etapa em dashboard completo.
- Não inventar gamificação aqui antes de registrar tentativas corretamente.
- Não quebrar contratos da API para apressar a UI.
- Não usar lógica duplicada no front para decidir regra de correção se isso pertence ao backend.

## Regra de bloqueio desta etapa
Sem tentativas reais persistidas, o dashboard posterior fica falso. O agente deve parar se o fluxo ainda depender de mock ou inconsistência de contrato.

## Regras técnicas específicas
- Regra de correção e registro precisa estar centralizada adequadamente.
- A UI pode orientar o usuário, mas não substituir a regra de negócio do backend.
- A etapa deve terminar com fluxo fim a fim realmente operacional.

## Entregas esperadas
- Tela de questão/resolução.
- Integração de envio e retorno.
- Persistência de tentativas via API.
- Visão mínima do histórico ou resultado.

## Critérios de pronto
- O aluno consegue responder questões do início ao fim.
- A tentativa fica persistida e refletida no sistema.
- Há feedback real ao usuário.
- A base de dados do progresso passa a existir de verdade.

## Validação obrigatória antes de aceitar a etapa
- Verificar persistência real das tentativas.
- Verificar coerência do feedback mostrado.
- Verificar se os dados gerados servem para alimentar o dashboard depois.

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
08 — Dashboard e progresso

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
07 — Fluxo Real de Estudo com Questões

Dependência obrigatória para iniciar:
Etapa 06 concluída e integração Web/API funcionando.

Regra de bloqueio:
Sem tentativas reais persistidas, o dashboard posterior fica falso. O agente deve parar se o fluxo ainda depender de mock ou inconsistência de contrato.

Objetivo da etapa:
entregar o primeiro ciclo completo de valor para o aluno: escolher, responder e registrar questões com feedback real

Escopo obrigatório:
- Implementar tela de resolução de questão.
- Permitir envio de resposta.
- Exibir feedback de acerto/erro.
- Registrar tentativa do usuário via API.
- Criar histórico simples ou visão mínima das tentativas.
- Permitir indicadores básicos por matéria ou assunto, se já estiverem previstos nos contratos.

Não faça nesta etapa:
- Não transformar essa etapa em dashboard completo.
- Não inventar gamificação aqui antes de registrar tentativas corretamente.
- Não quebrar contratos da API para apressar a UI.
- Não usar lógica duplicada no front para decidir regra de correção se isso pertence ao backend.

Regras técnicas específicas desta etapa:
- Regra de correção e registro precisa estar centralizada adequadamente.
- A UI pode orientar o usuário, mas não substituir a regra de negócio do backend.
- A etapa deve terminar com fluxo fim a fim realmente operacional.

Entregas esperadas:
- Tela de questão/resolução.
- Integração de envio e retorno.
- Persistência de tentativas via API.
- Visão mínima do histórico ou resultado.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O aluno consegue responder questões do início ao fim.
- A tentativa fica persistida e refletida no sistema.
- Há feedback real ao usuário.
- A base de dados do progresso passa a existir de verdade.

Checklist de validação antes de encerrar:
- Verificar persistência real das tentativas.
- Verificar coerência do feedback mostrado.
- Verificar se os dados gerados servem para alimentar o dashboard depois.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
