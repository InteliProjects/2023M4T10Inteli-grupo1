# Inteli - Instituto de Tecnologia e Liderança 

<p align="center">
<a href= "https://www.inteli.edu.br/"><img src="assets/inteli.png" alt="Inteli - Instituto de Tecnologia e Liderança" border="0" width=40% height=40%></a>
</p>

<br>

# IOTvos

## 👨‍🎓 Integrantes: 
- <a href="https://www.linkedin.com/in/anna-aragao/">Anna Aragão</a>
- <a href="https://www.linkedin.com/in/breno-santos-0843131b8/">Breno Santos</a>
- <a href="https://www.linkedin.com/in/davi-ferreira-arantes/">Davi Arantes </a> 
- <a href="https://www.linkedin.com/in/giovana-katsuki-murata-503a94264/">Giovanna Katsuki</a> 
- <a href="https://www.linkedin.com/in/jo%C3%A3o-hirata-085456279/">João Cauê</a>
- <a href="https://www.linkedin.com/in/vitto-mazeto/">Vitto Mazeto</a> 

## 👩‍🏫 Professores:
### Orientador(a) 
- <a href="https://www.linkedin.com/in/marcelo-gon%C3%A7alves-phd-a550652/">Marcelo Gonçalves</a>
### Instrutores
- <a href="https://www.linkedin.com/in/cristiano-benites-687647a8/">Cristiano Benites</a>
- <a href="https://www.linkedin.com/in/andreluizbraga/">André Luiz Braga</a> 
- <a href="https://www.linkedin.com/in/bruna-mayer-00a556174/">Bruna Mayer</a> 
- <a href="https://www.linkedin.com/in/claudio-andré-64911a1b5/">Claudio André</a>
- <a href="https://www.linkedin.com/in/fatima-toledo/">Fatima Toledo</a>
- <a href="https://www.linkedin.com/in/henrique-mohallem-paiva-6854b460/">Henrique Paiva</a>
- <a href="https://www.linkedin.com/in/juliastateri/">Julia Stateri</a>

## 📜 Descrição

### Resumo do Projeto

O projeto visa revolucionar a gestão de ativos da Atvos, uma indústria comprometida com a produção sustentável de energia. Utilizando a inovação da Internet das Coisas (IoT), busca-se implementar um sistema de monitoramento avançado para rastrear e fornecer informações precisas sobre a localização e estado dos equipamentos e recursos da empresa.

### Objetivos Principais

O objetivo principal é estabelecer um sistema de monitoramento detalhado para cada recurso, reduzindo ocorrências indesejadas, como roubos e perdas, enquanto melhora a eficiência operacional por meio da otimização de processos. A implantação da IoT permitirá a coleta de dados valiosos, criando um histórico confiável para embasar futuras decisões e aprimorar as operações da empresa.

### Proposta de Solução
A proposta de solução aborda os desafios atuais da Atvos, incluindo roubo, perda de estoque e monitoramento deficiente das operações de reparo. A solução proposta oferece:
- **Monitoramento em Tempo Real**: Dispositivos IoT integrados a equipamentos e peças permitirão o acompanhamento em tempo real da localização e estado dos ativos, reduzindo roubos e perdas.
- **Alertas e Notificações**: O sistema gerará alertas imediatos em caso de movimentação não autorizada, permitindo ação imediata para evitar prejuízos.
- **Eficiência na Manutenção**: Facilitará o agendamento e acompanhamento de manutenções preventivas, garantindo a disponibilidade contínua dos equipamentos.
- **Rastreamento de Histórico**: Registrará e armazenará o histórico de localização e manutenção, fornecendo informações cruciais para decisões futuras.
- **Gerenciamento de Estoque**: Automatizará o controle de estoque, rastreando todas as peças em tempo real e notificando quando a reposição for necessária.

### Implementação Técnica

A solução baseia-se na integração de dispositivos IoT, como ESP32, módulos GPS e leitores de RFID, nos ativos da empresa. Esses dispositivos coletam dados em tempo real, transmitindo-os para um servidor de backend. A plataforma web oferece uma interface amigável para acessar informações detalhadas sobre os ativos, alertas instantâneos e um histórico completo de movimentos.

### Impacto Esperado

A implementação bem-sucedida desta solução promete transformar a gestão de ativos da Atvos, aumentando sua eficácia, segurança e eficiência. Ao superar desafios atuais de segurança e monitoramento, a empresa poderá reduzir custos, melhorar a produtividade e garantir uma operação mais sustentável.

Link para vídeo de demonstração: https://drive.google.com/file/d/1gowLm4gR7RXSTpbaSLAOAuUsOdbkO9Hi/view?usp=sharing


## 📁 Estrutura de pastas

Dentre os arquivos e pastas presentes na raiz do projeto, definem-se:

- <b>assets</b>: aqui estão os arquivos relacionados a parte gráfica do projeto, ou seja, as imagens e vídeos que os representam (O logo do grupo pode ser adicionado nesta pasta).

- <b>document</b>: aqui estão todos os documentos do projeto, incluindo o manual de instruções (se aplicável). Há também uma pasta denominada <b>outros</b> onde estão presentes outros documentos complementares.

- <b>src</b>: Todo o código fonte criado para o desenvolvimento do projeto, incluindo firmware, notebooks, backend e frontend, se aplicáveis.

- <b>README.md</b>: arquivo que serve como guia e explicação geral sobre o projeto (o mesmo que você está lendo agora).

## 🔧 Instalação

### Pré-requisitos

#### Para o projeto .NET:

1. **.NET SDK 6.0:** Verifique se você possui o .NET SDK 6.0 instalado. Caso não tenha, você pode baixá-lo [aqui](https://dotnet.microsoft.com/download/dotnet/6.0).

#### Para os ESP (Arduino IDE):

1. **Arduino IDE:** Baixe e instale a última versão da [Arduino IDE](https://www.arduino.cc/en/Main/Software).

#### Para o Front-end (Node.js e React):

1. **Node.js:** Instale o Node.js, que inclui o npm (gerenciador de pacotes). Você pode baixar a versão mais recente [aqui](https://nodejs.org/).

### Instalação

#### Para o projeto .NET:

1. Abra o terminal e execute o seguinte comando para verificar a versão do .NET SDK:

    ```bash
    dotnet --version
    ```

2. Navegue até o diretório do seu projeto e execute os seguintes comandos:

    ```bash
    dotnet build
    dotnet run
    ```

#### Para os ESP (Arduino IDE):

1. Abra a Arduino IDE e vá para `File -> Preferences`.
2. Adicione a seguinte URL ao campo "Additional Boards Manager URLs": `http://arduino.esp8266.com/stable/package_esp8266com_index.json`.
3. Vá para `Tools -> Board -> Boards Manager`, pesquise por "esp8266" e instale o pacote `esp8266 by ESP8266 Community`.
4. Selecione sua placa em `Tools -> Board`, escolha a porta correta e carregue o código nos ESP.

#### Para o Front-end (Node.js e React):

1. Abra o terminal e execute os seguintes comandos para instalar o create-react-app globalmente (caso ainda não tenha):

    ```bash
    npm install -g create-react-app
    ```

2. Crie um novo aplicativo React:

    ```bash
    npx create-react-app meu-app
    ```

3. Navegue até o diretório do seu aplicativo React e inicie o servidor de desenvolvimento:

    ```bash
    cd meu-app
    npm start
    ```


Link para manual de instruções: https://docs.google.com/document/d/1eRw9G0Y3Syo9bQoxQ9WZrJIi087qN7RU/edit


## 🗃 Histórico de lançamentos

* **0.5.0 - 19/12/2023**
    * **IoT:**
        * Integração de novos sensores nos dispositivos ESP.
        * Testes extensivos para garantir a estabilidade do protótipo.

* **0.4.0 - 08/12/2023**
    * **Backend:**
        * Integração de banco de dados para armazenamento eficiente de dados.
        * Implementação de lógica de negócios adicional.
    * **Frontend:**
        * Melhorias na navegação e na interface do usuário.
        * Correções de bugs identificados em versões anteriores.
    * **IoT:**
        * Atualizações de firmware nos dispositivos ESP para suportar novos recursos.
        * Integração de protocolos de comunicação mais eficientes.

* **0.3.0 - 24/11/2023**
    * **Backend:**
        * Implementação inicial de APIs para interação com o frontend.
        * Configuração do ambiente de desenvolvimento.
    * **Frontend:**
        * Implementação inicial
        * Configuração do ambiente de desenvolvimento.

* **0.2.0 - 10/11/2023**
    * **Protótipo:**
        * Construção do protótipo físico inicial.
        * Integração de componentes básicos.
        * Testes iniciais de funcionalidade.

* **0.1.0 - 27/10/2023**
    * **Concepção do Projeto:**
        * Definição de requisitos e escopo.
        * Pesquisa e seleção de tecnologias.
        * Planejamento inicial do projeto.


## 📋 Licença/License
<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="https://github.com/2023M4T10Inteli/grupo1">IotVos</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/2023M4T10Inteli/grupo1">INTELI, ANNA ARAGÃO, BRENO SANTOS, DAVI ARANTES, GIOVANNA KATSUKI, JOÃO CAUE, VITTO MAZETO</a> is licensed under <a href="http://creativecommons.org/licenses/by/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">Attribution 4.0 International<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1"><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1"></a></p>
