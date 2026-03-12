# 08 — Dashboard e Progresso (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 07 concluída com tentativas reais registradas.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Transformar os dados de estudo em feedback útil ao aluno, consolidando progresso, desempenho e visão inicial de evolução.

## Escopo obrigatório
- Exibir quantidade de questões respondidas.
- Exibir acertos e erros.
- Exibir progresso por matéria ou categoria relevante.
- Exibir metas atuais e streak inicial, se o backend já suportar.
- Montar um dashboard útil, e não apenas decorativo.

## Fora de escopo / proibido nesta etapa
- Não usar números mockados.
- Não criar gráficos ou componentes exagerados sem dado confiável por trás.
- Não antecipar cronograma avançado ou gamificação completa nesta etapa.

## Regra de bloqueio desta etapa
Sem dados reais agregados, o dashboard vira maquiagem. O agente deve parar e apontar o que falta no backend/contratos para consolidar essas informações.

## Regras técnicas específicas
- Cada indicador exibido deve ter origem clara.
- Preferir clareza a excesso de visualização.
- Não inventar métricas que o sistema ainda não calcula.

## Entregas esperadas
- Views/componentes do dashboard.
- Integração com endpoints/serviços de progresso.
- Estados de carregamento e erro coerentes.

## Critérios de pronto
- O dashboard mostra progresso real baseado em dados reais.
- O aluno consegue entender o próprio desempenho.
- A etapa prepara o terreno para cronograma e engajamento posterior.
- A dashboard deixa de ser uma home vazia.

## Validação obrigatória antes de aceitar a etapa
- Verificar que os números exibidos batem com os registros reais.
- Verificar se o dashboard ajuda na tomada de decisão do aluno.
- Verificar se a estrutura permite expansão depois.

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
09 — Cronograma e plano de estudo

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
08 — Dashboard e Progresso

Dependência obrigatória para iniciar:
Etapa 07 concluída com tentativas reais registradas.

Regra de bloqueio:
Sem dados reais agregados, o dashboard vira maquiagem. O agente deve parar e apontar o que falta no backend/contratos para consolidar essas informações.

Objetivo da etapa:
transformar os dados de estudo em feedback útil ao aluno, consolidando progresso, desempenho e visão inicial de evolução

Escopo obrigatório:
- Exibir quantidade de questões respondidas.
- Exibir acertos e erros.
- Exibir progresso por matéria ou categoria relevante.
- Exibir metas atuais e streak inicial, se o backend já suportar.
- Montar um dashboard útil, e não apenas decorativo.

Não faça nesta etapa:
- Não usar números mockados.
- Não criar gráficos ou componentes exagerados sem dado confiável por trás.
- Não antecipar cronograma avançado ou gamificação completa nesta etapa.

Regras técnicas específicas desta etapa:
- Cada indicador exibido deve ter origem clara.
- Preferir clareza a excesso de visualização.
- Não inventar métricas que o sistema ainda não calcula.

Entregas esperadas:
- Views/componentes do dashboard.
- Integração com endpoints/serviços de progresso.
- Estados de carregamento e erro coerentes.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O dashboard mostra progresso real baseado em dados reais.
- O aluno consegue entender o próprio desempenho.
- A etapa prepara o terreno para cronograma e engajamento posterior.
- A dashboard deixa de ser uma home vazia.

Checklist de validação antes de encerrar:
- Verificar que os números exibidos batem com os registros reais.
- Verificar se o dashboard ajuda na tomada de decisão do aluno.
- Verificar se a estrutura permite expansão depois.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
