# ProcessadorJSON

O ProcessadorJSON é uma aplicação para processar arquivos CSV contendo registros de funcionários de diferentes departamentos. A aplicação lê esses arquivos, realiza cálculos específicos como:
- Total a receber
- Horas extras
- Horas de débito.
- Dias de Faltas
- Dias Extras
- Dias trabalhados
- Total de horas trabalhadas
e gera uma saída em formato JSON consolidando as informações dos departamentos.

# Funcionalidades
# 1. Leitura de Arquivos CSV
A aplicação lê arquivos CSV que contêm informações de funcionários, como:
- código
- nome
- valor da hora trabalhada
- data
- horário de entrada, saída, e almoço.
Formato dos Arquivos: Os arquivos devem estar no formato CSV, separados por ponto e vírgula (;).

# 2. Processamento de Dados
Ao processar os arquivos CSV, a aplicação calcula:
- Total de horas trabalhadas.
- Horas extras.
- Horas de débito.
- Total a receber.
Os cálculos são processados para cada funcionário com base nos dados fornecidos.

# 3. Geração de Saída em JSON
A aplicação gera um único arquivo JSON que contém todos os dados processados, agrupados por departamento.

# 4. API REST
A aplicação oferece uma API RESTful que permite a submissão de arquivos CSV para processamento e o retorno dos resultados em JSON.
ProcessarArquivos: Recebe o caminho de uma pasta contendo arquivos CSV e processa todos os arquivos dentro dessa pasta.

# 5. Tratamento de Erros
A aplicação faz uma validação dos dados antes do processamento, caso um dado não estiver no formato esperado, a aplicação loga o erro e continua o processamento para os demais registros.

# Como Executar o Projeto
Pré-requisitos:
- .NET 3.1 ou superior
- Abrir o arquivo ProcessadorJSON.sln com o Visual Studio
- Para iniciar a aplicação basta ir na aba Depurar(Debug) > Iniciar Depuração (F5)
- Para testar eu criei uma pasta no Desktop onde tinha os arquivos CSV, fui até a tela Processamento/Index.cshtml copiei o caminho da pasta no desktop e colei no Input e clicar no botão 'Processar'
- Utilizei os arquivos genericos CSV que estão no repositório para fazer o processamento.
- Recomendações: Limpar a Solução e Recompilar a Solução antes de Iniciar.

# Estrutura do Projeto
Controllers: Contém os controladores que lidam com as requisições HTTP.

Services: Contém a lógica de negócio para processar os arquivos CSV
- Chama o ProcessadorJSON.Utils.CSVHelper.
- Processa o arquivo CSV e criar os objetos DepartamentoModel e FuncionarioModel.
- Retirar nome do departamento, mês de vigência, ano de vigência do nome do arquivo.
- Calcula o total a ser pago para o departamento.
- Calcula o total de descontos para o departamento.
- Calcula o total de horas extras para o departamento.
Models: Contém as classes de modelo.

Utils: Contém classes utilitárias para:
- leitura de arquivos CSV
- Calcular o total a receber, horas extras, horas débito, etc.
- Implementa a lógica para calcular
  
Views: Contém as páginas CSHTML por onde vai ser necessário navegar entre as telas, para que a aplicação possa processar arquivos CSV.
- Home/Index.cshtml
- Home/About.cshtml
- Processamento/Index.cshtml contém um Input e Método POST para ser colocado o caminho da pasta do arquivo CSV para ser processado para JSON
