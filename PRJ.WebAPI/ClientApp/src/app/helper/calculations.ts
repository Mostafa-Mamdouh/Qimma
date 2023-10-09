// convert Bq to TBq: (Bq) * 1.0E-12
// convert Bq to MBq: (Bq) * 1.0E-6

// radioactive current activity calculator
// i: initial activity
// d: productionDate
// halfLife: half life time
export const currentActivty = (
  i,
  d,
  halfLife,
  halfLifeUnit = 'days',
  initialActivtyUnit = 'TBq'
) => {
  if (initialActivtyUnit === 'Bq') i = i * 1.0e-12;
  else if (initialActivtyUnit === 'MBq') i = i * 1.0e-6;

  let hl;
  if (halfLifeUnit == 'days') hl = halfLife;
  else if (halfLifeUnit == 'years') hl = halfLife * 365;
  else if (halfLifeUnit == 'seconds') hl = halfLife / (3600 * 24);
  else if (halfLifeUnit == 'minutes') hl = (halfLife % 3600) / 60;
  else if (halfLifeUnit == 'hours') hl = (halfLife % (3600 * 24)) / 3600;

  let currentDate: any = new Date();

  let t = Math.abs(currentDate - d) / (1000 * 60 * 60 * 24); // time in days
  return i * Math.pow(Math.E, (-0.693 * t) / hl);
};

// Category calculator
// a: current activity in TBq
// d: constant

export const sourceCategory = (a: any, d: any) => {
  console.log('Current Activity: ' + a.toExponential());
  let ar = Number(a) / Number(d);

  console.log('Activity Ratio: ' + ar.toExponential());

  switch (true) {
    case ar >= 1000:
      return 'category 1';
    case ar < 1000 && ar >= 10:
      return 'category 2';
    case ar < 10 && ar >= 1:
      return 'category 3';
    case ar < 1 && ar >= 0.01:
      return 'category 4';
    case ar < 0.01:
      return 'category 5';
    default:
      return 'wrong';
  }
};

// Ir-192
// console.log('Ir-192');
// console.log(
//   sourceCategory(
//     currentActivty(2.18e12, new Date('01/16/2022'), 73.831, 'days', 'Bq'),
//     8e-2
//   )
// );

// Co-57
// console.log('Co-57');
// console.log(
//   sourceCategory(
//     currentActivty(1.85, new Date('01/20/2022'), 271.79, 'days', 'MBq'),
//     7e-1
//   )
// );

// Co-60
// console.log('Co-60');
// console.log(
//   sourceCategory(
//     currentActivty(50, new Date('09/18/2001'), 5.26, 'years', 'TBq'),
//     3e-2
//   )
// );
