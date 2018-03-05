var build = require('gulp-build');
 
var options = {
  helpers: [{
    name: 'addition',
    fn: function(a, b) { return a + b; }
  }]
};
 
gulp.task('build', function() {
  gulp.src('pages/*.html')
      .pipe(build({ title: 'Some page' }, options))
      .pipe(gulp.dest('dist'))
});