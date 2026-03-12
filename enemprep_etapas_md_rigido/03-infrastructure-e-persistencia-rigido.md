# 03 — Infrastructure e Persistência (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapas 01 e 02 concluídas e estáveis.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Implementar persistência e integrações técnicas respeitando o modelo do domínio e os contratos da aplicação, sem invadir outras camadas.

## Escopo obrigatório
- Criar DbContext.
- Mapear entidades e relacionamentos.
- Implementar repositórios concretos e demais adaptadores necessários.
- Criar migrations iniciais e seed mínimo apenas se isso fizer parte da estratégia adotada.
- Configurar acesso a banco, registration de dependências e persistência.

## Fora de escopo / proibido nesta etapa
- Não reescrever entidades do Domain para facilitar o banco sem justificar.
- Não colocar regra de negócio no repositório.
- Não criar controller, tela ou fluxo web nesta etapa.
- Não alterar contratos da Application arbitrariamente para encaixar na persistência.

## Regra de bloqueio desta etapa
Sem persistência consistente, a API nasce instável. O agente deve parar se houver conflito entre Domain, Application e mapeamento.

## Regras técnicas específicas
- Priorizar configuração explícita e limpa.
- Se usar seed, mantê-lo mínimo, coerente e útil para desenvolvimento inicial.
- Não esconder fragilidades de modelagem com gambiarras no mapeamento.

## Entregas esperadas
- DbContext e configurações.
- Mapeamentos de entidades.
- Implementações de repositórios.
- Migrations e seed mínimo, se aplicável.
- Registro de dependências de infraestrutura.

## Critérios de pronto
- A persistência funciona e compila.
- Os repositórios implementam os contratos necessários.
- O banco representa o domínio sem distorções graves.
- A etapa deixa a solução pronta para a API expor fluxos reais.

## Validação obrigatória antes de aceitar a etapa
- Verificar se o banco representa os relacionamentos do domínio.
- Verificar se a Infrastructure não força a Application a conhecer detalhes do EF.
- Verificar se a etapa prepara endpoints reais, não só compila.

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
04 — API base

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
03 — Infrastructure e Persistência

Dependência obrigatória para iniciar:
Etapas 01 e 02 concluídas e estáveis.

Regra de bloqueio:
Sem persistência consistente, a API nasce instável. O agente deve parar se houver conflito entre Domain, Application e mapeamento.

Objetivo da etapa:
implementar persistência e integrações técnicas respeitando o modelo do domínio e os contratos da aplicação, sem invadir outras camadas

Escopo obrigatório:
- Criar DbContext.
- Mapear entidades e relacionamentos.
- Implementar repositórios concretos e demais adaptadores necessários.
- Criar migrations iniciais e seed mínimo apenas se isso fizer parte da estratégia adotada.
- Configurar acesso a banco, registration de dependências e persistência.

Não faça nesta etapa:
- Não reescrever entidades do Domain para facilitar o banco sem justificar.
- Não colocar regra de negócio no repositório.
- Não criar controller, tela ou fluxo web nesta etapa.
- Não alterar contratos da Application arbitrariamente para encaixar na persistência.

Regras técnicas específicas desta etapa:
- Priorizar configuração explícita e limpa.
- Se usar seed, mantê-lo mínimo, coerente e útil para desenvolvimento inicial.
- Não esconder fragilidades de modelagem com gambiarras no mapeamento.

Entregas esperadas:
- DbContext e configurações.
- Mapeamentos de entidades.
- Implementações de repositórios.
- Migrations e seed mínimo, se aplicável.
- Registro de dependências de infraestrutura.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- A persistência funciona e compila.
- Os repositórios implementam os contratos necessários.
- O banco representa o domínio sem distorções graves.
- A etapa deixa a solução pronta para a API expor fluxos reais.

Checklist de validação antes de encerrar:
- Verificar se o banco representa os relacionamentos do domínio.
- Verificar se a Infrastructure não força a Application a conhecer detalhes do EF.
- Verificar se a etapa prepara endpoints reais, não só compila.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
