[To do]
 - Adicionar banco de dados para salvar as opções de dropdown [ ]



[Index.cshtml] -> Exibição
Tem apenas o formulário para captar os dados do usuário que serão enviados para o
backend para criação do carimbo em JPG.

Aponta para o controller Carimbo, Ação Gerar, método POST (não usar get para não
receber um 405).

há selects para dropdown, com opções pré-definidas e há inputs de texto comuns para
os campos de texto livre.

por último há um botão input que faz a requisição para o backend.



[CarimboImageService.cs] -> Criação da Imagem
Há um método de gerar imagem que recebe um modelo
define o tamanho da tela,
instancia as classes e tipos necessários (Bitmap, Graphics, Font, Brushes),
define um ponto incial de desenho,
desenha um retângulo,
desenha uma string com os dados obtidos do front (texto, fonte, tipo de pincel, coordenada),
incrementa a coordenada,
repete os 2 passos anteriores para todas as escritas

por fim instancia uma MemoryStream, salva o bitmap na stream usando tipo Jpeg 
e retorna como um array de Bytes.


