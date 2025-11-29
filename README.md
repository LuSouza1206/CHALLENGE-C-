Health Track Pro - C# Console Application

Integrantes:
- Kaio Vinicius Meireles Alves - RM553282
- Lucas Alves de Souza -  RM553956
- Lucas de Freitas Pagung -  RM553242
- Guilherme Fernandes de Freitas - RM554323
- João Pedro Chizzolini de Freitas - RM553172
- 
Projeto desenvolvido para o módulo de Software Development (C#) do Challenge Care Plus. Trata-se de uma aplicação de console robusta para gestão de saúde e bem-estar.

Objetivo:
Criar uma aplicação que permita ao usuário registrar hábitos diários (como consumo de água, passos e horas de sono), visualizar um histórico organizado e acompanhar seu progresso através de um dashboard estatístico com metas personalizáveis.

Funcionalidades:

1.  Dashboard Inteligente: Exibe total, média diária e identifica automaticamente o Recorde (Máximo) de cada atividade.
2.  Metas Personalizáveis: O usuário não fica preso a números fixos. É possível configurar suas próprias metas (ex: alterar meta de água de 2000ml para 3000ml) através do menu de configurações.
3.  Visualização de Progresso: Feedback visual com barras de progresso que indicam se a meta foi batida ou quanto falta.
4.  Interface Otimizada (UX): Uso de cores no terminal (Verde para sucesso, Vermelho para alertas, Ciano para menus) e tabelas alinhadas para facilitar a leitura.
5.  Validação de Dados: O sistema blinda entradas inválidas (como textos em campos numéricos ou números negativos).

Estrutura do Projeto

 `Program.cs`: Contém toda a lógica da aplicação, estruturada em métodos (`AdicionarRegistro`, `ExibirDashboard`, `ConfigurarMetas`) e utilizando Listas em memória para gerenciamento dos dados.
 `HealthTrackApp.csproj`: Arquivo de configuração do projeto .NET.

Como Rodar o Projeto

1.  Certifique-se de ter o .NET SDK instalado.
2.  Abra o terminal na pasta do projeto.
3.  Execute o comando:

```bash
dotnet run
