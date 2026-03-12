# Contexto Base do Projeto — EnemPrep

## Resumo executivo
O **EnemPrep** é uma plataforma web de estudos para o ENEM com foco em prática, organização, acompanhamento de progresso e motivação do aluno. O produto reúne banco de questões, videoaulas, materiais, cronograma, desafios diários, streak e dashboard.

## Objetivo do produto
Ajudar estudantes a estudar de forma mais organizada, prática e consistente, centralizando:
- questões com alternativas e correção;
- videoaulas por matéria e assunto;
- materiais complementares;
- metas e cronograma;
- progresso, streak e engajamento.

## Arquitetura obrigatória
A solução deve permanecer separada em:
- **EnemPrep.Domain**
- **EnemPrep.Application**
- **EnemPrep.Infrastructure**
- **EnemPrep.API**
- **EnemPrep.Web**

## Regra de ouro da arquitetura
A **Web não pode referenciar Domain, Application ou Infrastructure diretamente**.  
A Web deve consumir a API via **HttpClient/JSON**.

## Stack principal
- C#
- .NET 10
- ASP.NET Core
- API REST
- Front Web desacoplado
- Persistência relacional

## Diretrizes permanentes
1. Manter separação clara entre camadas.
2. Não mover regra de negócio para controller.
3. Não acoplar Web a banco.
4. Não implementar etapas futuras sem necessidade explícita.
5. Não alterar arquitetura para “acelerar”.
6. Não remover código existente sem explicar impacto.
7. Toda mudança deve deixar a etapa atual pronta para servir de base à próxima.

## Ordem rígida de desenvolvimento
1. 00 — Visão geral e arquitetura
2. 01 — Modelagem do Domain
3. 02 — Contratos da Application
4. 03 — Infrastructure e persistência
5. 04 — API base
6. 05 — Admin MVP
7. 06 — Web do aluno: estrutura inicial
8. 07 — Fluxo real de estudo com questões
9. 08 — Dashboard e progresso
10. 09 — Cronograma e plano de estudo
11. 10 — Engajamento e gamificação
12. 11 — Refinamento de UX, performance e segurança
13. 12 — Pós-MVP

## Regra de dependência entre etapas
Uma etapa **não pode começar** se a etapa anterior:
- não compilar;
- não estiver coerente com a arquitetura;
- tiver pendências estruturais importantes;
- gerar dúvidas sobre contratos ou modelo de dados que impactam a etapa atual.

## Regra de parada do agente
Se a etapa anterior não estiver consistente, o agente deve:
1. **parar**;
2. explicar exatamente o bloqueio;
3. listar o que falta para liberar a etapa atual;
4. não improvisar solução estrutural fora do escopo sem justificar.

## Formato obrigatório de entrega do agente
Sempre responder com:
- caminho + conteúdo completo dos arquivos novos ou alterados;
- explicação curta da responsabilidade de cada arquivo;
- observações de impacto arquitetural;
- pendências e riscos;
- confirmação explícita de que **não avançou para a próxima etapa**.

## Restrições permanentes
- Não criar funcionalidades fora do escopo da etapa.
- Não “adiantar” camadas futuras.
- Não introduzir dependência circular.
- Não misturar DTO com entidade de domínio.
- Não usar dados mockados quando já existe backend real para a etapa.
- Não quebrar compatibilidade com .NET 10.
