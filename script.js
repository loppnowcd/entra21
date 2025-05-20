class Calculadora {
            somar(a, b) {
            return a + b;
            }

            subtrair(a, b) {
            return a - b;
            }

            multiplicar(a, b) {
            return a * b;
            }

            dividir(a, b) {
            return a / b;
            }
            }

            function calculo() {
            const valor1 = parseFloat(document.getElementById('primeiro-numero').value);
            const valor2 = parseFloat(document.getElementById('segundo-numero').value);
            const tipo = document.getElementById('tipoCalculo').value;
            const calc = new Calculadora();
            let resultado;

            if (tipo == "somar") {
            resultado = calc.somar(valor1, valor2);
            } else if (tipo == "subtrair") {
            resultado = calc.subtrair(valor1, valor2);
            } else if (tipo == "multiplicar") {
            resultado = calc.multiplicar(valor1, valor2);
            } else if (tipo == "dividir") {
            resultado = calc.dividir(valor1, valor2);
            }

            document.getElementById('resultado').value = resultado;
            }


            //Supondo que fosse fazer uma função sem classe
            //function chamarCalculo() {
            //let valor1 = document.getElementById('numValor1').value
            //let valor2 = document.getElementById("numValor2").value
            //let operacao = document.getElementById('selectOperacao').value

            //let resultadoOperacao = eval(parseInt(valor1) + operacao + parseInt(valor2))

            //let resultado = document.getElementById('numResultado')

            //resultado.value = resultadoOperacao
            //}

