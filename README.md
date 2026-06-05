# SpaceCare — Sistema Integrado de Monitoramento de Saúde para Turismo Espacial

## Índice

| [Motivação](#motivação-do-projeto) | [Integrantes](#integrantes--3espg) | [Integração](#integração-com-o-projeto) | [Tecnologias](#estruturas-arquitetura-e-tecnologias) | [Endpoints](#endpoints-da-api) | [Como Executar](#como-executar-o-projeto) | [Evidências](#evidências-de-execução) |

## Motivação do Projeto

O **SpaceCare** nasce como uma solução de telemedicina preventiva de alto escalão. A principal motivação é **salvar vidas por meio da antecipação de problemas**. Sensores isolados de IoT podem falhar ou gerar falsos positivos; por isso, nossa plataforma inova ao criar um ecossistema de **Inteligência Biométrica Cruzada**. Ao cruzar os dados vitais contínuos do passageiro com as reações físicas e gestos voluntários ou involuntários que ele esboça no ambiente, a equipe médica remota na Terra ganha uma precisão cirúrgica para triagem, diagnóstico e acionamento de alertas de emergência em tempo real.

---

## Integrantes — 3ESPG
* João Victor Soave **(RM557595)**
* Maria Alice Freitas Araújo **(RM557516)**
* Pedro Henrique Mendes dos Santos **(RM555332)**
* Rafael Teofilo Lucena	**(RM555600)**
* Vinicius Fernandes Tavares Bittencourt **(RM558909)**

---

## Integração com o Projeto
A API REST desenvolvida em C# (.NET) atua como o cérebro centralizador e unificado da operação, recebendo informações simultâneas de dois canais digitais distintos:

1. **Canal IoT Físico (Internet of Things):** Um hardware embarcado (baseado no microcontrolador ESP32) coleta continuamente os batimentos cardíacos e a temperatura corporal do turista através do traje espacial, transmitindo esses pacotes para a API por meio de requisições HTTP POST.
2. **Canal IoB Visual (Internet of Behaviors):** Câmeras internas de monitoramento capturam as expressões e movimentos do usuário. A biblioteca **MediaPipe** decodifica visualmente esses gestos médicos (ex: mão no peito, mão na cabeça). O software então envia imediatamente o gesto interpretado para a nossa API.

### Inteligência Biométrica Cruzada (Regras de Negócio)
Ao receber um comportamento IoB, o `ComportamentoService` busca no banco de dados a telemetria IoT mais recente do respectivo turista para contextualizar o quadro médico:
* Se o turista fizer o gesto de **Mão no Peito** e a última telemetria acusar **Batimentos >= 140 bpm** ou **Pressão Arterial >= 140 mmHg**, o sistema infere automaticamente um **Alerta Vermelho** (Risco Cardíaco Crítico).
* Se o turista colocar a **Mão na Cabeça** e a telemetria apontar **Temperatura >= 38°C** ou oscilações graves de pressão, gera-se um **Alerta Vermelho** (Risco Neurológico/Térmico).
* Caso os gestos sejam detectados sem alterações nos sinais vitais do IoT, o sistema estabiliza o quadro em **Alerta Amarelo** (Atenção Preventiva).

---

## Estruturas, Arquitetura e Tecnologias

### Modelagem de Domínio & POO
O projeto utiliza os conceitos de Programação Orientada a Objetos para isolar o domínio das regras de infraestrutura. As entidades fundamentais (`Turista`, `Telemetria`, `Comportamento`) possuem forte encapsulamento e propriedades fortemente tipadas, mapeando as regras de negócio.

### Abstração, Interfaces e Injeção de Dependência (DI)
Seguindo os princípios do **SOLID**, todas as camadas de serviço foram desacopladas de seus controladores através de contratos puros (Interfaces), localizadas dentro do próprio domínio:
* `ITuristaService` ➔ `TuristaService`
* `ITelemetriaService` ➔ `TelemetriaService`
* `IComportamentoService` ➔ `ComportamentoService`

Os controladores dependem estritamente das interfaces, garantindo testabilidade e inversão de controle gerenciada nativamente no conteiner do .NET.

### Tratamento Global de Exceções (Middleware)
Desenvolvemos o `TratadorGlobalErros` (um Middleware customizado) no pipeline do ASP.NET Core. Ele intercepta as requisições e trata exceções semânticas de negócio:
* **`TuristaNotFoundException`:** Mapeia e responde automaticamente um HTTP **404 Not Found** com o JSON padronizado.
* **`ArgumentException`:** Captura falhas de regras de entrada (como o formato da pressão arterial) e responde um HTTP **400 Bad Request**.

### Estruturas Auxiliares — DTOs via C# Records
Para a segurança do tráfego de rede, a API separa os modelos de banco de dados das estruturas de payload:
* **DTOs de Entrada (`Cadastrar...`):** Classes tradicionais com Data Annotations rígidas de validação (`[Required]`, `[Range]`, `[EnumDataType]`).
* **DTOs de Saída (`Detalhar...`):** Estruturados como **C# Records posicionais imutáveis**, blindando os dados contra mutações pós-leitura.

### Persistência e Conexão com Banco de Dados
Mapeamento Relacional (ORM) operando com o **Entity Framework Core**, utilizando atributos explícitos (`[Table]`, `[Column]`) para persistir, atualizar e ler dados diretamente de tabelas corporativas hospedadas em um banco **Oracle Database**.

### Deleção Lógica
A API implementa o padrão de exclusão lógica (Soft Delete) no campo `FL_ATIVO` (`Ativo = "1"` ou `"0"`). Um turista deletado nunca tem seus registros expurgados fisicamente, protegendo o histórico médico e bloqueando novas telemetrias para contas inativas.

---

## Endpoints da API

### Turistas (`/turistas`)
* `POST /turistas` — Cadastra um novo perfil de turista espacial.
* `GET /turistas` — Retorna a listagem simplificada de todos os turistas ativos.
* `GET /turistas/{id}` — Detalha o perfil completo e histórico de um turista por ID.
* `PUT /turistas` — Atualiza os dados cadastrais (Nome, Passaporte, Email, Histórico Médico).
* `DELETE /turistas/{id}` — Desativa logicamente o turista da plataforma (Soft Delete).

### Telemetrias IoT (`/telemetrias`)
* `POST /telemetrias` — Recebe e valida dados vitais (Batimentos, Temperatura, Pressão Arterial).
* `GET /telemetrias` — Retorna o painel geral de telemetrias capturadas de turistas ativos.
* `GET /telemetrias/turista/{turistaId}` — Exibe a linha do tempo cronológica de telemetrias de um passageiro específico.

### Comportamentos IoB (`/comportamentos`)
* `POST /comportamentos` — Registra um gesto (`0=Polegar Cima`, `1=Polegar Baixo`, `2=Mão Cabeça`, `3=Mão Peito`), processa a triagem de alerta e salva o resultado.
* `GET /comportamentos` — Lista o histórico geral de análises de comportamento da tripulação.
* `GET /comportamentos/turista/{turistaId}` — Retorna o histórico de alertas e gestos de um único turista.

---

## Como Executar o Projeto

### 1. Configurando o `appsettings.Development.json`
Abra o arquivo `appsettings.Development.json` localizado na raiz do projeto e altere a string de conexão para apontar para as credenciais do seu banco Oracle:

```json
"ConnectionStrings": {
  "OracleConnection": "Data Source=URL_BANCO;User Id=USUARIO;Password=SENHA;"
}
```

### 2. Executando no terminal
```bash
# Restaura os pacotes e dependências NuGet do SpaceCare
dotnet restore

# Compila a aplicação e inicia o servidor
dotnet run --launch-profile "Development"
```

---

## Evidências de Execução

### 1. Endpoints Turistas

| **Cadastrar** | **Listar** | **Deletar** |
| :---: | :---: | :---: |
| ![cadastrarTurista](./Docs/Images/cadastrarTurista.png) | ![listarTurista](./Docs/Images/listarTurista.png) | ![deletarTurista](./Docs/Images/deletarTurista.png) |

### 2. Endpoints Telemetrias

| **Cadastrar**  | **Listar** |
| :---: | :---: |
| ![cadastrarTelemetria](./Docs/Images/cadastrarTelemetria.png) | ![listarTelemetria](./Docs/Images/listarTelemetria.png) |

### 3. Endpoints Comportamentos

| **Cadastrar** | **Listar** |
| :---: | :---: |
| ![cadastrarComportamento](./Docs/Images/cadastrarComportamento.png) | ![listarComportamento](./Docs/Images/listarComportamento.png) |

### 4. Retorno Erros

| **Conflito** | **Turista Inexistente** |
| :---: | :---: |
| ![erroConflito](./Docs/Images/erroConflito.png) | ![erroTuristaInexistente](./Docs/Images/erroTuristaInexistente.png) |