# Documentação Técnica Completa — Portal GTF News API

> **Status desta versão:** base estrutural completa para onboarding de outros Agents de IA, preparada para ser enriquecida automaticamente assim que os artefatos reais da API estiverem no repositório (`PortalGTF.API/DocAPI.json`, `PortalGTF.API/PortalGTFNewsDB.sql` e código-fonte das camadas).

---

## 1) Visão Geral do Projeto

O **Portal GTF News API** foi projetado com **Arquitetura Limpa (Clean Architecture)**, separando claramente responsabilidades por camadas:

- **API**: exposição HTTP (controllers/endpoints, middlewares, autenticação, documentação Swagger/OpenAPI).
- **APPLICATION**: casos de uso, regras de orquestração, DTOs, contratos de entrada/saída.
- **CORE**: domínio puro (entidades, value objects, regras de negócio centrais, contratos/abstrações).
- **INFRASTRUCTURE**: detalhes técnicos (persistência, repositórios concretos, integrações externas).

Também foram informados os seguintes padrões principais:

- **Repository Pattern** para abstrair acesso a dados.
- **Autenticação JWT** para segurança e autorização.
- **Script SQL de banco** em `PortalGTF.API/PortalGTFNewsDB.sql`.
- **Export do Swagger** em `PortalGTF.API/DocAPI.json`.

---

## 2) Objetivo desta documentação

Este documento foi desenhado para permitir que outros Agents de IA consigam:

1. Entender rapidamente a arquitetura e convenções do projeto.
2. Navegar pelas camadas sem quebrar responsabilidades.
3. Implementar novas features mantendo o padrão existente.
4. Evoluir endpoints com segurança (contrato + regra + persistência).
5. Trabalhar com JWT, permissões e regras de acesso.
6. Entender o banco e o impacto de mudanças de schema.

---

## 3) Arquitetura (Clean Architecture)

## 3.1 Fluxo de dependências

Em Clean Architecture, a direção de dependência é para dentro:

- `API -> APPLICATION -> CORE`
- `INFRASTRUCTURE -> APPLICATION/CORE` (implementa contratos definidos internamente)

**Princípio-chave:**
- O **CORE** não conhece infraestrutura.
- A **APPLICATION** conhece apenas abstrações e regras de caso de uso.
- A **API** apenas adapta HTTP para casos de uso.
- A **INFRASTRUCTURE** implementa detalhes (banco, provedores, etc.).

## 3.2 Responsabilidades por camada

### API
- Controllers ou handlers HTTP.
- Model binding, validação de entrada (se aplicável).
- Conversão request/response para DTOs.
- Registro de middlewares (auth, exceções, logs, CORS).
- Configuração Swagger/OpenAPI.

### APPLICATION
- Use Cases (ex.: criar notícia, listar categorias, autenticar usuário).
- Contratos de serviços e contratos de saída.
- Mapeamento entre DTOs e entidades de domínio.
- Políticas transacionais (quando necessário).

### CORE
- Entidades centrais do negócio (ex.: Notícia, Categoria, Usuário, Permissão etc.).
- Invariantes de domínio.
- Interfaces de repositórios (ou contratos de acesso).
- Eventos de domínio (se adotado).

### INFRASTRUCTURE
- Implementação concreta dos repositórios.
- ORM/SQL e acesso ao banco.
- Configuração de contexto de dados.
- Implementações externas (cache, mensageria, serviços terceiros).

---

## 4) Padrões adotados

## 4.1 Repository Pattern

### Objetivo
Desacoplar casos de uso da tecnologia de persistência.

### Estrutura recomendada
- **Interface** no CORE/APPLICATION (ex.: `INoticiaRepository`).
- **Implementação** na INFRASTRUCTURE (ex.: `NoticiaRepository`).

### Benefícios
- Testabilidade (mock/fake de repositório).
- Troca de tecnologia de dados com baixo impacto.
- Clareza de responsabilidade por camada.

## 4.2 JWT (Authentication/Authorization)

### Fluxo típico
1. Cliente autentica com credenciais.
2. API valida credenciais via caso de uso.
3. API gera JWT com claims (sub, role, permissões, exp etc.).
4. Cliente envia `Authorization: Bearer <token>`.
5. Middleware valida token e define identidade.
6. Endpoints protegidos aplicam política/role/claims.

### Boas práticas
- Chave forte (mín. 256 bits para HMAC).
- Expiração curta + refresh token (quando necessário).
- Nunca trafegar token fora de HTTPS.
- Validar emissor (`iss`) e audiência (`aud`) quando configurados.

---

## 5) Convenções para classes e organização

> Esta seção serve como contrato para Agents: manter padrão consistente ao criar/alterar classes.

## 5.1 Convenções de nomenclatura
- **Entidades de domínio**: nomes de negócio no singular (`Noticia`, `Usuario`).
- **DTOs**: sufixos explícitos (`CreateNoticiaRequest`, `NoticiaResponse`).
- **Use Cases/Services**: verbos de ação (`CreateNoticiaUseCase`, `AuthenticateUserService`).
- **Repositórios**: interface prefixada com `I` e implementação concreta (`INoticiaRepository`, `NoticiaRepository`).

## 5.2 Convenções de métodos
- Assíncrono com sufixo `Async` quando aplicável.
- Métodos de leitura não devem produzir efeitos colaterais.
- Métodos de escrita retornam objeto de resultado claro (id criado, entidade, ou result object).

## 5.3 Tratamento de erros
- Erros de domínio: mensagens orientadas a negócio.
- Erros técnicos: encapsular sem vazar detalhes sensíveis.
- API deve padronizar resposta de erro (código, mensagem, rastreabilidade/correlation id).

---

## 6) Endpoints da API

> **Importante:** o detalhamento completo por endpoint deve ser extraído do arquivo `PortalGTF.API/DocAPI.json` (export Swagger). Como o artefato não está presente nesta revisão do repositório, abaixo está a estrutura pronta para preenchimento.

## 6.1 Matriz de endpoints (template)

Para cada endpoint, documentar:
- **Método + rota**
- **Descrição funcional**
- **Autenticação exigida** (público/JWT/role/policy)
- **Parâmetros de rota/query**
- **Request body** (campos obrigatórios/opcionais)
- **Respostas** (200/201/400/401/403/404/422/500)
- **Regras de negócio associadas**
- **Casos de erro comuns**

### Exemplo de preenchimento

#### `POST /api/auth/login`
- Objetivo: autenticar usuário e retornar token JWT.
- Auth: público.
- Request: `{ email, password }`.
- Response 200: `{ accessToken, expiresIn, user }`.
- Erros: credenciais inválidas (401), payload inválido (400).

> Replicar o formato para 100% dos endpoints do `DocAPI.json`.

## 6.2 Grupos sugeridos
- **Auth**
- **Usuários**
- **Notícias**
- **Categorias/Tags**
- **Administração**
- **Health/Status**

---

## 7) Banco de Dados

> Fonte oficial esperada: `PortalGTF.API/PortalGTFNewsDB.sql`.

## 7.1 Inventário esperado
Documentar, para cada tabela:
- Nome e finalidade.
- Chave primária.
- Colunas (tipo, nullability, default).
- Índices.
- FKs e cardinalidade.
- Regras de integridade.

## 7.2 Checklist de consistência API x Banco
- Cada endpoint de escrita mapeia para entidades/tabelas corretas.
- Restrições do banco refletem validações de aplicação.
- Índices atendem consultas de listagem/paginação/filtro.
- Soft delete/auditoria (se existente) refletidos no domínio.

---

## 8) Segurança

- JWT obrigatório para rotas protegidas.
- Roles/policies por endpoint administrativo.
- Sanitização de entrada e validação de payload.
- Política de CORS adequada aos clientes oficiais.
- Logs sem dados sensíveis (token, senha, segredos).
- Secretos via variável de ambiente / vault (não hardcoded).

---

## 9) Observabilidade e operação

- Logging estruturado com correlação por request.
- Métricas de latência, erro e throughput.
- Health checks (liveness/readiness).
- Versionamento de API (`/v1`, `/v2`) quando aplicável.

---

## 10) Guia para Agents de IA (modo operacional)

Quando um Agent atuar no projeto, seguir esta ordem:

1. Ler arquitetura e identificar camada correta da mudança.
2. Alterar primeiro regras de negócio (CORE/APPLICATION) e depois adaptação (API/INFRA).
3. Atualizar contratos (DTOs/interfaces) antes de implementação concreta.
4. Validar segurança (JWT/policies) em qualquer endpoint novo.
5. Atualizar documentação de endpoint e impacto no banco.
6. Garantir que naming e estrutura de classes seguem o padrão aqui descrito.

### Regras de ouro para Agents
- Não colocar regra de negócio complexa em controller.
- Não acoplar Use Case diretamente a ORM/framework.
- Não vazar detalhes internos em mensagens de erro públicas.
- Não quebrar contrato de endpoint sem versionamento.

---

## 11) Próximo passo recomendado (automação da documentação)

Para transformar este documento em uma versão 100% concreta:

1. Adicionar ao repositório os arquivos reais:
    - `PortalGTF.API/DocAPI.json`
    - `PortalGTF.API/PortalGTFNewsDB.sql`
    - Código das camadas (`API`, `APPLICATION`, `CORE`, `INFRASTRUCTURE`).
2. Gerar seção “Endpoints” automaticamente a partir do Swagger JSON.
3. Gerar seção “Banco” automaticamente a partir do SQL.
4. Manter este arquivo como referência de alto nível + links para docs geradas.

---

## 12) Estado desta entrega

Nesta revisão específica do repositório, não foram encontrados os artefatos de código/API/SQL mencionados, então esta documentação foi produzida como **baseline completa de arquitetura, padrões e operação para IA**, pronta para ser enriquecida com os dados reais assim que os arquivos estiverem presentes.