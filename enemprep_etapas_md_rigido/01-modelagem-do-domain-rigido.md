# 01 — Modelagem do Domain (Versão Rígida)

## Finalidade deste arquivo
Este arquivo `.md` deve ser usado para instruir um agente do Antigravity com **restrições rígidas**.
Ele existe para evitar que o agente:
- misture camadas;
- pule etapas;
- implemente funcionalidades fora do escopo;
- crie dependências erradas;
- entregue código grande, mas estruturalmente fraco.

## Dependência obrigatória
Etapa 00 concluída, compilando e com arquitetura validada.

## Regra dura de progressão
**Esta etapa não autoriza trabalho da próxima.**
O agente só pode executar esta etapa se a anterior estiver:
- compilando;
- coerente com a arquitetura;
- sem pendência estrutural bloqueante.

Se isso não estiver verdadeiro, o agente deve **parar** e relatar o bloqueio.
Ele **não pode improvisar** uma solução fora do escopo só para seguir em frente.

## Objetivo da etapa
Implementar o núcleo do negócio com entidades, enums, relações e invariantes básicas, sem qualquer acoplamento com persistência ou ui.

## Escopo obrigatório
- Criar entidades centrais: Usuario, PerfilUsuario, Materia, Assunto, Questao, Alternativa, TentativaQuestao, VideoAula, MaterialEstudo, PlanoEstudo, PlanoEstudoItem, DesafioDiario, StreakUsuario, Conquista e UsuarioConquista.
- Criar enums necessários, como cargo/perfil e demais classificações de domínio realmente úteis.
- Representar relacionamentos 1:1, 1:N e N:N conforme o modelo do produto.
- Adicionar validações de consistência e regras simples de integridade do domínio.

## Fora de escopo / proibido nesta etapa
- Não criar DbContext ou mapeamentos EF Core.
- Não criar DTOs, services de Application ou controllers.
- Não adicionar atributos específicos de banco sem justificativa muito forte.
- Não colocar lógica de infraestrutura no Domain.
- Não criar classes 'anêmicas' demais sem qualquer proteção mínima do estado.

## Regra de bloqueio desta etapa
Se o Domain estiver ambíguo ou inconsistente, a Application ficará errada. Nesse caso, o agente deve parar e apontar o bloqueio em vez de seguir.

## Regras técnicas específicas
- Entidade não é DTO e não deve ser desenhada pensando na tela.
- Se usar construtores/fábricas/métodos de domínio, eles devem proteger o estado e não inflar artificialmente o modelo.
- A modelagem deve refletir o PRD, não apenas um CRUD genérico.

## Entregas esperadas
- Arquivos das entidades.
- Arquivos de enums.
- Possíveis value objects, se forem realmente necessários e justificáveis.
- Organização interna do projeto Domain.

## Critérios de pronto
- O projeto Domain compila isoladamente.
- As entidades representam o negócio com clareza e relação coerente.
- A modelagem está pronta para ser consumida pela Application e persistida depois pela Infrastructure.
- As regras básicas de integridade não foram empurradas para etapas futuras sem motivo.

## Validação obrigatória antes de aceitar a etapa
- Verificar independência do Domain.
- Verificar coerência dos relacionamentos.
- Verificar se não há detalhe de persistência infiltrado na modelagem.
- Verificar se os nomes e responsabilidades são consistentes.

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
02 — Contratos da Application

## Prompt rígido pronto para enviar ao Antigravity

```txt
Você é um agente especialista em C# e .NET 10 trabalhando no projeto EnemPrep.

Contexto fixo do projeto:
- Plataforma web de estudos para o ENEM.
- Arquitetura obrigatória: Domain, Application, Infrastructure, API e Web.
- A Web consome a API via HttpClient/JSON e não pode referenciar Domain, Application ou Infrastructure diretamente.
- O projeto deve respeitar a ordem rígida de etapas do roadmap.

Etapa atual:
01 — Modelagem do Domain

Dependência obrigatória para iniciar:
Etapa 00 concluída, compilando e com arquitetura validada.

Regra de bloqueio:
Se o Domain estiver ambíguo ou inconsistente, a Application ficará errada. Nesse caso, o agente deve parar e apontar o bloqueio em vez de seguir.

Objetivo da etapa:
implementar o núcleo do negócio com entidades, enums, relações e invariantes básicas, sem qualquer acoplamento com persistência ou UI

Escopo obrigatório:
- Criar entidades centrais: Usuario, PerfilUsuario, Materia, Assunto, Questao, Alternativa, TentativaQuestao, VideoAula, MaterialEstudo, PlanoEstudo, PlanoEstudoItem, DesafioDiario, StreakUsuario, Conquista e UsuarioConquista.
- Criar enums necessários, como cargo/perfil e demais classificações de domínio realmente úteis.
- Representar relacionamentos 1:1, 1:N e N:N conforme o modelo do produto.
- Adicionar validações de consistência e regras simples de integridade do domínio.

Não faça nesta etapa:
- Não criar DbContext ou mapeamentos EF Core.
- Não criar DTOs, services de Application ou controllers.
- Não adicionar atributos específicos de banco sem justificativa muito forte.
- Não colocar lógica de infraestrutura no Domain.
- Não criar classes 'anêmicas' demais sem qualquer proteção mínima do estado.

Regras técnicas específicas desta etapa:
- Entidade não é DTO e não deve ser desenhada pensando na tela.
- Se usar construtores/fábricas/métodos de domínio, eles devem proteger o estado e não inflar artificialmente o modelo.
- A modelagem deve refletir o PRD, não apenas um CRUD genérico.

Entregas esperadas:
- Arquivos das entidades.
- Arquivos de enums.
- Possíveis value objects, se forem realmente necessários e justificáveis.
- Organização interna do projeto Domain.

Formato obrigatório da resposta:
- Entregar por arquivos, com caminho + conteúdo completo dos arquivos novos ou alterados.
- Explicar rapidamente a responsabilidade de cada arquivo.
- Listar impactos técnicos, pendências e riscos.
- Informar explicitamente se houve necessidade de ajustar arquivos fora do escopo — e justificar.
- Confirmar no final que não avançou para a próxima etapa sem autorização.

Critérios de pronto:
- O projeto Domain compila isoladamente.
- As entidades representam o negócio com clareza e relação coerente.
- A modelagem está pronta para ser consumida pela Application e persistida depois pela Infrastructure.
- As regras básicas de integridade não foram empurradas para etapas futuras sem motivo.

Checklist de validação antes de encerrar:
- Verificar independência do Domain.
- Verificar coerência dos relacionamentos.
- Verificar se não há detalhe de persistência infiltrado na modelagem.
- Verificar se os nomes e responsabilidades são consistentes.

Ao final da resposta, informe obrigatoriamente:
1. o que foi implementado
2. o que não foi implementado
3. quais pendências bloqueiam a próxima etapa, se houver
4. qual é a próxima etapa permitida do roadmap
5. confirmação explícita de que você não adiantou trabalho de outra etapa
```
